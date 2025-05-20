using BoardAPI.Models;
using BoardAPI.Models.Dtos;
using BoardAPI.Repositories.Interfaces;
using BoardAPI.Services.Interfaces;

namespace BoardAPI.Services
{
    public class AnnouncementsService : IAnnouncementsService
    {
        private readonly IAnnouncementsRepository repository;

        public AnnouncementsService(IAnnouncementsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AnnouncementDto> CreateAnnouncementAsync(AnnouncementDto announcement)
        {
            var id = await this.repository.CreateAnnouncementAsync(announcement);
            var createdAnnouncement = await this.repository.GetAnnouncementByIdAsync(id);
            return createdAnnouncement!;
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            var rowsAffected = await this.repository.DeleteAnnouncementAsync(id);
            return rowsAffected > 0;
        }

        public async Task<AnnouncementDto?> GetAnnouncementByIdAsync(int id)
        {
            return await this.repository.GetAnnouncementByIdAsync(id);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAnnouncementsAsync(AnnouncementFilter filter)
        {
            return await this.repository.GetAnnouncementsAsync(filter);
        }

        public async Task<AnnouncementDto> UpdateAnnouncementAsync(AnnouncementDto announcement)
        {
            var rowsAffected = await this.repository.UpdateAnnouncementAsync(announcement);
            if (rowsAffected == 0)
            {
                throw new KeyNotFoundException($"Announcement with ID {announcement.Id} not found");
            }

            return (await this.repository.GetAnnouncementByIdAsync(announcement.Id)) !;
        }
    }
}
