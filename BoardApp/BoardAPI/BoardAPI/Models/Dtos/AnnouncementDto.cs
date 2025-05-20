namespace BoardAPI.Models.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Status { get; set; }

        public string Category { get; set; } = null!;

        public string? SubCategory { get; set; }
    }
}