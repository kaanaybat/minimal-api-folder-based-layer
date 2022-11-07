using FluentValidation.Results;

namespace MinimalApi.Service.Extensions
{
public static class ValidationExtensions
{
    public static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult)
    {
      return validationResult.Errors
        .GroupBy(x => x.PropertyName)
        .ToDictionary(
          g => g.Key,
          g => g.Select(x => x.ErrorMessage).ToArray()
        );
    }

    public static List<string> ToList(this ValidationResult validationResult)
    {
      return validationResult.Errors.Select(x => x.ErrorMessage).ToList();
    }

}



}