using BoardAPI.Models.Validations;

namespace BoardAPI.Models
{
    public class AnnouncementFilter
    {
        public bool? Status { get; set; }

        [CategoryValidation]
        public string? Category { get; set; }

        [SubCategoryValidation]
        public string? SubCategory { get; set; }
    }
}