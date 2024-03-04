using System.ComponentModel.DataAnnotations;
using AzureFunctionCSharpCrud.Validators;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AzureFunctionCSharpCrud.Extensions;

public static class HttpRequestExtension
{
    public static async Task<BodyValidator<T>> ParseBody<T>(this HttpRequest request)
    {
        var body = await new StreamReader(request.Body).ReadToEndAsync();

        var validator = new BodyValidator<T>
        {
            Body = JsonConvert.DeserializeObject<T>(body)
        };

        var results = new List<ValidationResult>();
        validator.IsValid = Validator.TryValidateObject(
            validator.Body,
            new ValidationContext(validator.Body, null, null),
            results, true);
        validator.Errors = results;

        return validator;
    }
}