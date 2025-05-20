using System.ComponentModel.DataAnnotations;
using BoardAPI.Models.Constants;

namespace BoardAPI.Models.Validations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class SubCategoryValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        // Get the category value from the containing model
        var categoryProperty = validationContext.ObjectInstance.GetType().GetProperty("Category");
        var categoryValue = categoryProperty?.GetValue(validationContext.ObjectInstance)?.ToString();

        if (string.IsNullOrEmpty(categoryValue))
        {
            return new ValidationResult("Category must be specified when providing SubCategory");
        }

        var subCategory = value.ToString();

        return BillboardCategories.IsValidSubcategory(categoryValue, subCategory)
            ? ValidationResult.Success
            : new ValidationResult(GetErrorMessage(categoryValue, subCategory));
    }

    private static string GetErrorMessage(string category, string? invalidSubCategory)
    {
        var validSubCategories = BillboardCategories.GetSubcategoriesFor(category);
        return $"Invalid subcategory '{invalidSubCategory}' for category '{category}'. Valid options are: {string.Join(", ", validSubCategories)}";
    }
}
