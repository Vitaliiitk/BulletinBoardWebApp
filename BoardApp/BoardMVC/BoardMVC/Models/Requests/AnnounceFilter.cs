namespace BoardMVC.Models.Requests
{
    public class AnnounceFilter
    {
        public bool? Status { get; set; }

        public string? Category { get; set; }

        public string? SubCategory { get; set; }

        public IReadOnlyDictionary<string, IReadOnlyList<string>> AvailableCategories => CategoryData.AvailableCategories;

        public IReadOnlyList<string>? AvailableSubCategories { get; set; }

        public void UpdateSubCategories()
        {
            this.AvailableSubCategories = CategoryData.GetSubCategories(this.Category);
        }
    }
}