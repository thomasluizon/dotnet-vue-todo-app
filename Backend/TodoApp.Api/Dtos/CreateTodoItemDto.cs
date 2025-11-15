using System.ComponentModel.DataAnnotations;

namespace TodoApp.Api.Dtos;

public class CreateTodoItemDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
}
