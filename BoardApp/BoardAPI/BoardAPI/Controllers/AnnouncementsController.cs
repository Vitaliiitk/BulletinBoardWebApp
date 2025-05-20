using BoardAPI.Models;
using BoardAPI.Models.Dtos;
using BoardAPI.Models.Requests;
using BoardAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoardAPI.Controllers
{
    [ApiController]
    [Route("api/announcements")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementsService service;

        public AnnouncementsController(IAnnouncementsService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<AnnouncementDto>> CreateAnnouncement([FromBody] CreateAnnouncementRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var dto = new AnnouncementDto
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Category = request.Category,
                SubCategory = request.SubCategory,
            };

            var result = await this.service.CreateAnnouncementAsync(dto);
            return this.CreatedAtAction(nameof(this.GetAnnouncement), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                var success = await this.service.DeleteAnnouncementAsync(id);
                if (!success)
                {
                    return this.NotFound();
                }

                return this.NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementDto>> GetAnnouncement(int id)
        {
            try
            {
                var announcement = await this.service.GetAnnouncementByIdAsync(id);
                if (announcement == null)
                {
                    return this.NotFound();
                }

                return this.Ok(announcement);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementDto>>> GetAnnouncements([FromQuery] AnnouncementFilter filter)
        {
            try
            {
                var announcements = await this.service.GetAnnouncementsAsync(filter);
                return this.Ok(announcements);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] UpdateAnnouncementRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Get existing announcement
            var existingDto = await this.service.GetAnnouncementByIdAsync(id);
            if (existingDto == null)
            {
                return this.NotFound();
            }

            // Update non-null fields
            var mergedDto = new AnnouncementDto
            {
                Id = id,
                Title = request.Title ?? existingDto.Title,
                Description = request.Description,
                Status = request.Status ?? existingDto.Status,
                Category = request.Category ?? existingDto.Category,
                SubCategory = request.SubCategory,
                CreateDate = existingDto.CreateDate,
            };

            var result = await this.service.UpdateAnnouncementAsync(mergedDto);
            return this.Ok(result);
        }
    }
}