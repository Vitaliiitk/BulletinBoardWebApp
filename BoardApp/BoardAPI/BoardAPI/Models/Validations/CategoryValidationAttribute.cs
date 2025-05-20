using System.ComponentModel.DataAnnotations;
using BoardAPI.Models.Constants;

namespace BoardAPI.Models.Validations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class CategoryValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var category = value.ToString();

        return BillboardCategories.IsValidCategory(category)
            ? ValidationResult.Success
            : new ValidationResult(GetErrorMessage(category));
    }

    private static string GetErrorMessage(string? invalidCategory)
    {
        var validCategories = BillboardCategories.GetAllCategories();
        return $"Invalid category '{invalidCategory}'. Valid categories are: {string.Join(", ", validCategories)}";
    }
}