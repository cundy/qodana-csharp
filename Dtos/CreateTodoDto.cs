using System.ComponentModel.DataAnnotations;

namespace AzureFunctionCSharpCrud.Dtos;

public class CreateTodoDto
{
    [Required]
    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; }
}