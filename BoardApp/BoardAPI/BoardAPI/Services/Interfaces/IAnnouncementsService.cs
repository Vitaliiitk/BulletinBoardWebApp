using BoardAPI.Models;
using BoardAPI.Models.Dtos;

namespace BoardAPI.Services.Interfaces
{
    public interface IAnnouncementsService
    {
        Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementDto announcement);

        Task<IEnumerable<AnnouncementDto>> GetAnnouncementsAsync(AnnouncementFilter filter);

        Task<AnnouncementDto?> GetAnnouncementByIdAsync(int id);

        Task<AnnouncementDto> UpdateAnnouncementAsync(AnnouncementDto announcement);

        Task<bool> DeleteAnnouncementAsync(int id);
    }
}
