using AzureFunctionCSharpCrud.DbContexts;
using AzureFunctionCSharpCrud.Entities;
using AzureFunctionCSharpCrud.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AzureFunctionCSharpCrud.Services;

public class TodoRepository(DatabaseContext databaseContext) : ITodoRepository
{
    private DatabaseContext _dbContext = databaseContext;
    private readonly DbSet<TodoEntity> _todosContext = databaseContext.Todos;

    public async Task<TodoEntity> CreateTodo(string title, bool completed)
    {
        var todo = new TodoEntity()
        {
            Title = title,
            Completed = completed
        };
        _todosContext.Add(todo);
        await _dbContext.SaveChangesAsync();

        return todo;
    }

    public async Task DeleteTodo(int id) =>
        await _todosContext.Where(c => c.Id == id).ExecuteDeleteAsync();

    public async Task<TodoEntity?> GetTodo(int id) =>
        await _todosContext.SingleOrDefaultAsync(t => t.Id == id);

    public async Task<List<TodoEntity>> GetTodos() => await _todosContext.ToListAsync();

    public async Task<List<TodoEntity>> GetTodosDone(bool isDone = false)
    {
        var param = new SqlParameter("@completed", isDone);
        var sql = "EXECUTE [dbo].[StoredProcedureName] @completed";

        return await _todosContext.FromSqlRaw(sql, param).ToListAsync();
    }

    public async Task<TodoEntity?> UpdateTodo(int id, string title, bool completed)
    {
        var todo = await GetTodo(id);
        if (todo is null)
            return null;

        todo.Title = title;
        todo.Completed = completed;
        _todosContext.Update(todo);
        await _dbContext.SaveChangesAsync();

        return todo;
    }
}
