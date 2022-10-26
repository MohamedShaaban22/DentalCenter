using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.NewsImages.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.NewsImage
{
    public class NewsImageRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public NewsImageRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<int> DeleteByNewsIDAsync(Guid newsID)
        {
            string query = "Delete from [newsimage] where [newsid] = @newsID";
            var result = await _db.ExecuteAsync(query, new { newsID });
            return result;
        }

        public async Task<int> AddAsync(IEnumerable<NewsImagesBasicModel> model)
        {
            string query = @"INSERT INTO [dbo].[newsimage] ([imagepath], [newsid], [insertdate]) 
                                VALUES (@imagepath, @newsid, @insertdate)";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }
    }
}