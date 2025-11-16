using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Dtos;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TasksController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
    {
        var todos = await _todoService.GetAllTodosAsync();

        var todoDtos = todos.Select(t => new TodoItemDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted
        });

        return Ok(todoDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetById(int id)
    {
        var todo = await _todoService.GetTodoByIdAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        var todoDto = new TodoItemDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted
        };

        return Ok(todoDto);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItemDto>> Create([FromBody] CreateTodoItemDto createDto)
    {
        var createdTodo = await _todoService.CreateTodoAsync(createDto.Title, createDto.Description);

        var todoDto = new TodoItemDto
        {
            Id = createdTodo.Id,
            Title = createdTodo.Title,
            Description = createdTodo.Description,
            IsCompleted = createdTodo.IsCompleted
        };

        return CreatedAtAction(nameof(GetById), new { id = todoDto.Id }, todoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoItemDto updateDto)
    {
        var updatedTodo = await _todoService.UpdateTodoAsync(id, updateDto.Title, updateDto.Description, updateDto.IsCompleted);

        if (updatedTodo == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _todoService.DeleteTodoAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
