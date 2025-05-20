using System.ComponentModel.DataAnnotations;

namespace BoardMVC.Models.ViewModels
{
    public class CreateAnnouncementViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = null!;

        [StringLength(4000, ErrorMessage = "Description cannot exceed 4000 characters")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Make announcement active.")]
        public bool Status { get; set; } = true;

        [Required(ErrorMessage = "Please select a category")]
        public string Category { get; set; } = null!;

        public string? SubCategory { get; set; }

        public IReadOnlyDictionary<string, IReadOnlyList<string>> AvailableCategories => CategoryData.AvailableCategories;

        public IReadOnlyList<string>? AvailableSubCategories { get; set; }

        public void UpdateSubCategories()
        {
            this.AvailableSubCategories = CategoryData.GetSubCategories(this.Category);
        }
    }
}
