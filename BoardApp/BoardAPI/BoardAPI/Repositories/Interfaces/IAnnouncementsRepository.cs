using BoardAPI.Models;
using BoardAPI.Models.Dtos;

namespace BoardAPI.Repositories.Interfaces
{
    public interface IAnnouncementsRepository
    {
        Task<int> CreateAnnouncementAsync(AnnouncementDto announcement);

        Task<IEnumerable<AnnouncementDto>> GetAnnouncementsAsync(AnnouncementFilter filter);

        Task<AnnouncementDto?> GetAnnouncementByIdAsync(int id);

        Task<int> UpdateAnnouncementAsync(AnnouncementDto announcement);

        Task<int> DeleteAnnouncementAsync(int id);
    }
}