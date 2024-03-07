using System.ComponentModel.DataAnnotations;

namespace AzureFunctionCSharpCrud.Validators;

public class BodyValidator<T>
{
    public bool IsValid { get; set; }

    public T Body { get; set; }

    public IEnumerable<ValidationResult> Errors { get; set; }
}
