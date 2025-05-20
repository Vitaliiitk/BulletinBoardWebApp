namespace BoardMVC.Models
{
    public static class CategoryData
    {
        public static IReadOnlyDictionary<string, IReadOnlyList<string>> AvailableCategories { get; } =
            new Dictionary<string, IReadOnlyList<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["Household appliances"] = new List<string> { "Refrigerators", "Washing machines", "Boilers", "Ovens", "Microwaves", "Kitchen hoods" }.AsReadOnly(),
                ["Computers"] = new List<string> { "PC", "Laptops", "Monitors", "Printers", "Scanners" }.AsReadOnly(),
                ["Smartphones"] = new List<string> { "Android", "iOS/Apple" }.AsReadOnly(),
                ["Other"] = new List<string> { "Clothing", "Shoes", "Accessories", "Sports equipment", "Toys" }.AsReadOnly(),
            };

        public static IReadOnlyList<string>? GetSubCategories(string? category)
        {
            return !string.IsNullOrEmpty(category) ? AvailableCategories.FirstOrDefault(x => x.Key.Equals(category, StringComparison.OrdinalIgnoreCase)).Value : null;
        }
    }
}
