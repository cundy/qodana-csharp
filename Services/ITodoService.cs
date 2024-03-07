using AzureFunctionCSharpCrud.Dtos;

namespace AzureFunctionCSharpCrud.Services;

public interface ITodoService
{
    Task<TodoDto?> GetTodo(int id);

    Task<List<TodoDto>> GetTodos();

    Task<List<TodoDto>> GetTodosDone();

    Task<List<TodoDto>> GetTodosStarted();

    Task<TodoDto> CreateTodo(string title, bool completed);

    Task<TodoDto> UpdateTodo(int id, string title, bool completed);

    Task DeleteTodo(int id);
}
