using BoardMVC.Models.Requests;
using BoardMVC.Models.Responses;
using BoardMVC.Models.ViewModels;
using BoardMVC.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BoardMVC.Services
{
    public class AnnounceService : IAnnounceService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<AnnounceService> logger;

        public AnnounceService(IHttpClientFactory httpClientFactory, ILogger<AnnounceService> logger)
        {
            this.httpClient = httpClientFactory.CreateClient("ApiClient");
            this.logger = logger;
        }

        public async Task<bool> CreateAnnouncementAsync(CreateAnnouncementRequest request)
        {
            try
            {
                var jsonContent = JsonContent.Create(request);
                var response = await this.httpClient.PostAsync("/api/announcements", jsonContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error creating announcement");
                return false;
            }
        }

        public async Task<bool> DeleteAnnouncementAsync(int id)
        {
            try
            {
                var response = await this.httpClient.DeleteAsync($"/api/announcements/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                this.logger.LogError(ex, "HTTP error while deleting announcement ID {AnnouncementId}", id);
                return false;
            }
        }

        public async Task<AnnouncementViewModel> GetAnnouncementByIdAsync(int id)
        {
            var response = await this.httpClient.GetAsync($"/api/announcements/{id}");
            response.EnsureSuccessStatusCode();

            var announcementResponse = await response.Content.ReadFromJsonAsync<AnnounceResponse>();

            return new AnnouncementViewModel
            {
                Id = id,
                Title = announcementResponse!.Title,
                Description = announcementResponse.Description,
                CreateDate = announcementResponse.CreateDate,
                StatusValue = announcementResponse.Status,
                StatusDisplay = announcementResponse.Status ? "Active" : "Inactive",
                Category = announcementResponse.Category,
                SubCategory = announcementResponse.SubCategory,
            };
        }

        public async Task<IEnumerable<AnnouncementListViewModel>> GetAnnouncementsAsync(AnnounceFilter filter)
        {
            // Build query parameters
            var queryParams = new List<string>();

            if (filter.Status.HasValue)
            {
                queryParams.Add($"status={filter.Status.Value}");
            }

            if (!string.IsNullOrEmpty(filter.Category))
            {
                queryParams.Add($"category={Uri.EscapeDataString(filter.Category)}");
            }

            if (!string.IsNullOrEmpty(filter.SubCategory))
            {
                queryParams.Add($"subCategory={Uri.EscapeDataString(filter.SubCategory)}");
            }

            var endpoint = "/api/announcements" + (queryParams.Any() ? $"?{string.Join("&", queryParams)}" : string.Empty);
            var response = await this.httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var announcementDto = await response.Content.ReadFromJsonAsync<IEnumerable<AnnounceResponse>>();

            return new List<AnnouncementListViewModel>
            {
                new AnnouncementListViewModel
                {
                    Announcements = announcementDto?.Select(dto => new AnnouncementViewModel
                    {
                        Id = dto.Id,
                        Title = dto.Title,
                        Description = dto.Description,
                        CreateDate = dto.CreateDate,
                        StatusDisplay = dto.Status ? "Active" : "Inactive",
                        StatusValue = dto.Status,
                        Category = dto.Category,
                        SubCategory = dto.SubCategory,
                    }) ?? Enumerable.Empty<AnnouncementViewModel>(),
                    Filter = filter,
                },
            };
        }

        public async Task<bool> UpdateAnnouncementAsync(int id, UpdateAnnouncementRequest request)
        {
            try
            {
                var jsonContent = JsonContent.Create(request);
                var response = await this.httpClient.PatchAsync($"/api/announcements/{id}", jsonContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error updating announcement ID {AnnouncementId}", id);
                return false;
            }
        }
    }
}