using System.ComponentModel.DataAnnotations;

namespace BoardMVC.Models.ViewModels
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy HH:mm}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Status")]
        public string StatusDisplay { get; set; } = null!; // "Active"/"Inactive"

        public bool StatusValue { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; } = null!;

        [Display(Name = "Subcategory")]
        public string? SubCategory { get; set; }

        public string ShortDescription => !string.IsNullOrEmpty(this.Description) && this.Description.Length > 100 ? this.Description.Substring(0, 100) + "..." : this.Description ?? string.Empty;
    }
}
