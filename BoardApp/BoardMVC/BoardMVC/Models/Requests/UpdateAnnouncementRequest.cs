﻿namespace BoardMVC.Models.Requests
{
    public class UpdateAnnouncementRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; }

        public string? Category { get; set; }

        public string? SubCategory { get; set; }
    }
}