using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureFunctionCSharpCrud.Entities;

[Table("todo")]
public class TodoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool Completed { get; set; }

    public UserEntity? User { get; set; }
}