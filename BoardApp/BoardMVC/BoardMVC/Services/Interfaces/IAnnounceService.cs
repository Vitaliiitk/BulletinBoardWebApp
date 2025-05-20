using BoardMVC.Models.Requests;
using BoardMVC.Models.ViewModels;

namespace BoardMVC.Services.Interfaces
{
    public interface IAnnounceService
    {
        Task<IEnumerable<AnnouncementListViewModel>> GetAnnouncementsAsync(AnnounceFilter filter);

        Task<AnnouncementViewModel> GetAnnouncementByIdAsync(int id);

        Task<bool> CreateAnnouncementAsync(CreateAnnouncementRequest request);

        Task<bool> UpdateAnnouncementAsync(int id, UpdateAnnouncementRequest request);

        Task<bool> DeleteAnnouncementAsync(int id);
    }
}