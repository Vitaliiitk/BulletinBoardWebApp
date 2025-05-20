namespace BoardMVC.Models.Requests
{
    public class CreateAnnouncementRequest
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Category { get; set; } = null!;

        public string? SubCategory { get; set; }

        public bool Status { get; set; } = true;
    }
}
