using System.ComponentModel.DataAnnotations;
using BoardAPI.Models.Validations;

namespace BoardAPI.Models.Requests
{
    public class CreateAnnouncementRequest
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(4000)]
        public string? Description { get; set; }

        public bool Status { get; set; } = true;

        [Required]
        [CategoryValidation]
        public string Category { get; set; } = null!;

        [SubCategoryValidation]
        public string? SubCategory { get; set; }
    }
}