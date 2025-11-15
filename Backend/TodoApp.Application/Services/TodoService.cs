using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services;

public class TodoService : ITodoService
{
    private readonly IRepository<TodoItem> _repository;

    public TodoService(IRepository<TodoItem> repository)
    {
        _repository = repository;
    }

    public async Task<TodoItem?> GetTodoByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TodoItem> CreateTodoAsync(string title, string? description)
    {
        var todoItem = new TodoItem
        {
            Title = title,
            Description = description,
            IsCompleted = false
        };

        return await _repository.AddAsync(todoItem);
    }

    public async Task<TodoItem?> UpdateTodoAsync(int id, string title, string? description, bool isCompleted)
    {
        var existingTodo = await _repository.GetByIdAsync(id);

        if (existingTodo == null)
        {
            return null;
        }

        existingTodo.Title = title;
        existingTodo.Description = description;
        existingTodo.IsCompleted = isCompleted;

        await _repository.UpdateAsync(existingTodo);

        return existingTodo;
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        var existingTodo = await _repository.GetByIdAsync(id);

        if (existingTodo == null)
        {
            return false;
        }

        await _repository.DeleteAsync(existingTodo);

        return true;
    }
}
