using BoardMVC.Models.Requests;
using BoardMVC.Models.ViewModels;
using BoardMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoardMVC.Controllers
{
    public class AnnounceController : Controller
    {
        private readonly IAnnounceService announcementsService;
        private readonly ILogger<AnnounceController> logger;

        public AnnounceController(IAnnounceService announcementsService, ILogger<AnnounceController> logger)
        {
            this.announcementsService = announcementsService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AnnounceFilter filter)
        {
            filter.UpdateSubCategories();

            if (!this.ModelState.IsValid)
            {
                foreach (var error in this.ModelState.Values.SelectMany(v => v.Errors))
                {
                    this.logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
                }

                return this.View(new AnnouncementListViewModel
                {
                    Announcements = Enumerable.Empty<AnnouncementViewModel>(),
                    Filter = filter,
                });
            }

            try
            {
                var viewModels = await this.announcementsService.GetAnnouncementsAsync(filter);
                return this.View(viewModels.FirstOrDefault());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error loading announcements");
                return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AnnouncementDetails(int id)
        {
            try
            {
                var announcement = await this.announcementsService.GetAnnouncementByIdAsync(id);
                if (announcement == null)
                {
                    return this.NotFound();
                }

                return this.View(announcement);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error loading announcement with ID {AnnouncementId}", id);
                return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                bool deleteSuccessful = await this.announcementsService.DeleteAnnouncementAsync(id);

                if (deleteSuccessful)
                {
                    this.TempData["DeleteSuccess"] = "Announcement was deleted successfully";
                    return this.RedirectToAction(nameof(this.Index));
                }
                else
                {
                    this.TempData["DeleteError"] = "Failed to delete announcement";
                    return this.RedirectToAction(nameof(this.AnnouncementDetails), new { id });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error deleting announcement with ID {AnnouncementId}", id);
                this.TempData["DeleteError"] = "An unexpected error occurred";
                return this.RedirectToAction(nameof(this.AnnouncementDetails), new { id });
            }
        }

        [HttpGet]
        public IActionResult CreateAnnouncement()
        {
            var model = new CreateAnnouncementViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnnouncement(CreateAnnouncementViewModel model)
        {
            model.UpdateSubCategories();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var request = new CreateAnnouncementRequest
            {
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                SubCategory = model.SubCategory,
                Status = model.Status,
            };

            bool success = await this.announcementsService.CreateAnnouncementAsync(request);

            if (success)
            {
                this.TempData["SuccessMessage"] = "Announcement created successfully";
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ModelState.AddModelError(string.Empty, "Failed to create announcement.");
            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAnnouncement(int id)
        {
            try
            {
                var announcement = await this.announcementsService.GetAnnouncementByIdAsync(id);

                var model = new UpdateAnnouncementViewModel
                {
                    Id = announcement.Id,
                    Title = announcement.Title,
                    Description = announcement.Description,
                    Status = announcement.StatusValue,
                    Category = announcement.Category,
                    SubCategory = announcement.SubCategory,
                };

                model.UpdateSubCategories();
                return this.View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error loading announcement for update");
                return this.RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAnnouncement(UpdateAnnouncementViewModel model)
        {
            model.UpdateSubCategories();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var request = new UpdateAnnouncementRequest
            {
                Title = model.Title,
                Description = model.Description,
                Status = model.Status,
                Category = model.Category,
                SubCategory = model.SubCategory,
            };

            bool success = await this.announcementsService.UpdateAnnouncementAsync(model.Id, request);

            if (success)
            {
                this.TempData["SuccessMessage"] = "Announcement updated successfully";
                return this.RedirectToAction(nameof(this.AnnouncementDetails), new { id = model.Id });
            }

            this.ModelState.AddModelError(string.Empty, "Failed to update announcement");
            return this.View(model);
        }
    }
}
