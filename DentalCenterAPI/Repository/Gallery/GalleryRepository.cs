using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Gallery.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Gallery
{
    public class GalleryRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public GalleryRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<GalleryBasicModel>> GetAllAsync(string type)
        {
            string query = "SELECT * from gallery ";
            if (!string.IsNullOrEmpty(type) && type.ToLower() != "all")
                query += @"Where LOWER(type) = @type ";
            query += "order by insertdate ";
            var result = await _db.QueryAsync<GalleryBasicModel>(query, new { type });
            return result;
        }

        public async Task<bool> IsExistByIDAsync(Guid galleryID)
        {
            string query = @"Select count(1) from [gallery] where [galleryid] = @galleryID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { galleryID });
            return result;
        }

        public async Task<int> AddAsync(IEnumerable<GalleryBasicModel> entity)
        {
            string query = @"INSERT INTO [dbo].[gallery] ([type] ,[imagepath], [isfavorite] ,[insertdate])
                                VALUES (@type, @imagepath, @isfavorite, @insertdate)";
            var result = await _db.ExecuteAsync(query, entity);
            return result;
        }

        public async Task<int> UpdateTypeAsync(Guid galleryID, string type)
        {
            string query = @"UPDATE [gallery] SET [type] = @type 
                             where [galleryid] = @galleryID";
            var result = await _db.ExecuteAsync(query, new { galleryID, type });
            return result;
        }

        public async Task<int> UpdateIsFavoriteAsync(Guid galleryID, string type, bool isFavorite)
        {
            string query = @"UPDATE [gallery] SET isfavorite = @isFavorite 
                             where [galleryid] = @galleryID and LOWER([type]) = @type ";
            var result = await _db.ExecuteAsync(query, new { galleryID, type, isFavorite });
            return result;
        }


        public async Task<int> DeleteAsync(Guid galleryID)
        {
            string query = "Delete from [gallery] where [galleryid] = @galleryID";
            var result = await _db.ExecuteAsync(query, new { galleryID });
            return result;
        }

        public async Task<int> DeleteAsync(IEnumerable<Guid> galleryIDs)
        {
            string query = "Delete from [gallery] where [galleryid] IN @galleryIDs";
            var result = await _db.ExecuteAsync(query, new { galleryIDs });
            return result;
        }
    }
}