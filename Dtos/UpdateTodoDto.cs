using System.ComponentModel.DataAnnotations;

namespace AzureFunctionCSharpCrud.Dtos;

public class UpdateTodoDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public bool IsDone { get; set; }
}