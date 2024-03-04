using AzureFunctionCSharpCrud.Dtos;
using AzureFunctionCSharpCrud.Entities;
using AzureFunctionCSharpCrud.Repositories;

namespace AzureFunctionCSharpCrud.Services;

public class TodoService(ITodoRepository todoRepository) : ITodoService
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<TodoDto?> GetTodo(int id)
    {
        var todoEntity = await _todoRepository.GetTodo(id);
        return EntityToDto(todoEntity);
    }

    public async Task<List<TodoDto>> GetTodos()
    {
        var todoEntities = await _todoRepository.GetTodos();
        return todoEntities.Select(EntityToDto).ToList();
    }

    public async Task<List<TodoDto>> GetTodosDone()
    {
        var todoEntities = await _todoRepository.GetTodosDone(true);
        return todoEntities.Select(EntityToDto).ToList();
    }

    public async Task<List<TodoDto>> GetTodosStarted()
    {
        var todoEntities = await _todoRepository.GetTodosDone();
        return todoEntities.Select(EntityToDto).ToList();
    }

    public async Task<TodoDto> CreateTodo(string title, bool completed)
    {
        var todo = await _todoRepository.CreateTodo(title, completed);
        return EntityToDto(todo);
    }

    public async Task DeleteTodo(int id)
    {
        await _todoRepository.DeleteTodo(id);
    }

    public async Task<TodoDto> UpdateTodo(int id, string title, bool completed)
    {
        var todo = await _todoRepository.UpdateTodo(id, title, completed);
        return EntityToDto(todo!);
    }

    static private TodoDto? EntityToDto(TodoEntity? entity)
    {
        // TODO: automapper?
        if (entity == null)
        {
            return null;
        }
        return new()
        {
            Id = entity.Id,
            Title = entity.Title,
            IsDone = entity.Completed,
        };
    }
}
