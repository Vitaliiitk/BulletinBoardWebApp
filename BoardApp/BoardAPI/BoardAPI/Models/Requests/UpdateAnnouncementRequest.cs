using System.ComponentModel.DataAnnotations;
using BoardAPI.Models.Validations;

namespace BoardAPI.Models.Requests
{
    public class UpdateAnnouncementRequest
    {
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(4000)]
        public string? Description { get; set; }

        public bool? Status { get; set; }

        [CategoryValidation]
        public string? Category { get; set; }

        [SubCategoryValidation]
        public string? SubCategory { get; set; }
    }
}