using AzureFunctionCSharpCrud.Dtos;
using AzureFunctionCSharpCrud.Extensions;
using AzureFunctionCSharpCrud.Services;
using AzureFunctionCSharpCrud.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
// using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace AzureFunctionCSharpCrud.Functions;

public class TodoFunction(ILogger<TodoFunction> logger, ITodoService todoService)
{
    const string _route = "todos";
    const string _routeWithId = $"{_route}/{{id:int}}";
    private readonly ILogger<TodoFunction> _logger = logger;
    private readonly ITodoService _todoService = todoService;

    [Function(nameof(GetTodos))]
    public async Task<IActionResult> GetTodos(
        [HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethod.Get), Route = _route)]
        HttpRequest req,
        bool? done)
    {
        _logger.LogInformation("list of todos");

        var todos = done switch
        {
            true => await _todoService.GetTodosDone(),
            false => await _todoService.GetTodosStarted(),
            _ => await _todoService.GetTodos(),
        };

        return new OkObjectResult(todos);
    }

    [Function(nameof(GetTodoById))]
    public async Task<IActionResult> GetTodoById(
        [HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethod.Get), Route = _routeWithId)]
        HttpRequest req,
         int id)
    {
        _logger.LogInformation($"Get todo with id: '{id}'");
        var todo = await _todoService.GetTodo(id);

        return new OkObjectResult(todo);
    }

    [Function(nameof(CreateTodo))]
    public async Task<IActionResult> CreateTodo(
        [HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethod.Post), Route = _route)]
        HttpRequest req
        /*, [FromBody] CreateTodoDto body */)
    {
        var validator = await req.ParseBody<CreateTodoDto>();
        if (!validator.IsValid)
        {
            return GetErrorResult(validator);
        }

        var body = validator.Body;
        _logger.LogInformation($"Create a new todo with title: '{body.Title}'");
        var todo = await _todoService.CreateTodo(body.Title, body.IsDone);

        return new OkObjectResult(todo);
    }

    [Function(nameof(UpdateTodo))]
    public async Task<IActionResult> UpdateTodo(
        [HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethod.Put), Route = _route)]
        HttpRequest req
        /*, [FromBody] UpdateTodoDto body */)
    {
        var validator = await req.ParseBody<UpdateTodoDto>();
        if (!validator.IsValid)
        {
            return GetErrorResult(validator);
        }

        var body = validator.Body;
        _logger.LogInformation($"Update a todo item with title: '{body.Title}'");
        var todo = await _todoService.UpdateTodo(body.Id, body.Title, body.IsDone);

        return new OkObjectResult(todo);
    }

    [Function(nameof(DeleteTodo))]
    public async Task<IActionResult> DeleteTodo([HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethod.Delete), Route = _routeWithId)]
    HttpRequest req,
    int id)
    {
        _logger.LogInformation($"delete todo with id: '{id}'");
        await _todoService.DeleteTodo(id);

        return new NoContentResult();
    }

    // TODO: make it more generic ex: middleware, etc...
    private static BadRequestObjectResult GetErrorResult<T>(BodyValidator<T> validator)
    {
        var errrors = validator.Errors.Select(s => s.ErrorMessage).ToArray();
        return new BadRequestObjectResult($"Invalid input: {string.Join(", ", errrors)}");
    }
}
