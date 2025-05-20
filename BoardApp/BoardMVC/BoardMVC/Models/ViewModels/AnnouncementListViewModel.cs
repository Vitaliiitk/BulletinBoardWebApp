using BoardMVC.Models.Requests;

namespace BoardMVC.Models.ViewModels
{
    public class AnnouncementListViewModel
    {
        public IEnumerable<AnnouncementViewModel>? Announcements { get; set; }

        public AnnounceFilter? Filter { get; set; }
    }
}
