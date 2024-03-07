using AzureFunctionCSharpCrud.Entities;

namespace AzureFunctionCSharpCrud.Repositories;

public interface ITodoRepository
{
    Task<TodoEntity?> GetTodo(int id);

    Task<List<TodoEntity>> GetTodos();

    Task<List<TodoEntity>> GetTodosDone(bool isDone = false);

    Task<TodoEntity> CreateTodo(string title, bool completed);

    Task<TodoEntity?> UpdateTodo(int id, string title, bool completed);

    Task DeleteTodo(int id);
}
