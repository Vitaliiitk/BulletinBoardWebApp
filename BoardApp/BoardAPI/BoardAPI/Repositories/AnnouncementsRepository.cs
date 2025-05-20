using System.Data;
using BoardAPI.Models;
using BoardAPI.Models.Dtos;
using BoardAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace BoardAPI.Repositories
{
    public class AnnouncementsRepository : IAnnouncementsRepository
    {
        private readonly string connectionString;

        public AnnouncementsRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("BillboardDbConnection") ?? throw new InvalidOperationException("Connection string 'BillboardDbConnection' is missing.");
        }

        public async Task<int> CreateAnnouncementAsync(AnnouncementDto announcement)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand("storedProcedure_CreateAnnouncement", connection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.AddWithValue("@Title", announcement.Title);
            command.Parameters.AddWithValue("@Description", announcement.Description != null ? (object)announcement.Description : DBNull.Value);
            command.Parameters.AddWithValue("@Status", announcement.Status);
            command.Parameters.AddWithValue("@Category", announcement.Category);
            command.Parameters.AddWithValue("@SubCategory", announcement.SubCategory != null ? (object)announcement.SubCategory : DBNull.Value);

            await connection.OpenAsync();

            try
            {
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Failed to create announcement in the database.", ex);
            }
        }

        public async Task<int> DeleteAnnouncementAsync(int id)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand("storedProcedure_DeleteAnnouncement", connection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();

            try
            {
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database error occurred while deleting announcement", ex);
            }
        }

        public async Task<AnnouncementDto?> GetAnnouncementByIdAsync(int id)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand("storedProcedure_GetAnnouncementById", connection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new AnnouncementDto
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    CreateDate = reader.GetDateTime(3),
                    Status = reader.GetBoolean(4),
                    Category = reader.GetString(5),
                    SubCategory = reader.IsDBNull(6) ? null : reader.GetString(6),
                };
            }

            return null;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAnnouncementsAsync(AnnouncementFilter filter)
        {
            var announcements = new List<AnnouncementDto>();

            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand("storedProcedure_GetAnnouncements", connection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.AddWithValue("@Status", filter.Status ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Category", filter.Category ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@SubCategory", filter.SubCategory ?? (object)DBNull.Value);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                announcements.Add(new AnnouncementDto
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    CreateDate = reader.GetDateTime(3),
                    Status = reader.GetBoolean(4),
                    Category = reader.GetString(5),
                    SubCategory = reader.IsDBNull(6) ? null : reader.GetString(6),
                });
            }

            return announcements;
        }

        public async Task<int> UpdateAnnouncementAsync(AnnouncementDto announcement)
        {
            using var connection = new SqlConnection(this.connectionString);
            using var command = new SqlCommand("storedProcedure_UpdateAnnouncement", connection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.AddWithValue("@Id", announcement.Id);
            command.Parameters.AddWithValue("@Title", announcement.Title);
            command.Parameters.AddWithValue("@Description", announcement.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Status", announcement.Status);
            command.Parameters.AddWithValue("@Category", announcement.Category);
            command.Parameters.AddWithValue("@SubCategory", announcement.SubCategory ?? (object)DBNull.Value);

            await connection.OpenAsync();

            try
            {
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database error occurred while updating announcement", ex);
            }
        }
    }
}
