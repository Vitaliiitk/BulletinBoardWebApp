namespace BoardAPI.Models.Constants
{
    public static class BillboardCategories
    {
        public static readonly IReadOnlyDictionary<string, IReadOnlyList<string>> AllowedCategories =
            new Dictionary<string, IReadOnlyList<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["Household appliances"] = new List<string> { "Refrigerators", "Washing machines", "Boilers", "Ovens", "Microwaves", "Kitchen hoods" }.AsReadOnly(),
                ["Computers"] = new List<string> { "PC", "Laptops", "Monitors", "Printers", "Scanners" }.AsReadOnly(),
                ["Smartphones"] = new List<string> { "Android", "iOS/Apple" }.AsReadOnly(),
                ["Other"] = new List<string> { "Clothing", "Shoes", "Accessories", "Sports equipment", "Toys" }.AsReadOnly(),
            };

        public static bool IsValidCategory(string? category) => !string.IsNullOrWhiteSpace(category) && AllowedCategories.ContainsKey(category);

        public static bool IsValidSubcategory(string? category, string? subCategory)
        {
            if (string.IsNullOrWhiteSpace(subCategory))
            {
                return true;
            }

            if (!IsValidCategory(category))
            {
                return false;
            }

            var allowedSubs = AllowedCategories[category!];
            return allowedSubs.Count == 0 || allowedSubs.Contains(subCategory, StringComparer.OrdinalIgnoreCase);
        }

        public static List<string> GetAllCategories() => AllowedCategories.Keys.OrderBy(k => k).ToList();

        public static List<string> GetSubcategoriesFor(string? category)
            => !string.IsNullOrWhiteSpace(category) && AllowedCategories.TryGetValue(category, out var subs)
                ? subs.OrderBy(s => s).ToList()
                : new List<string>();
    }
}
