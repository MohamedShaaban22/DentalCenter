using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.News.Basic;
using DentalCenterAPI.Models.News.Business;
using DentalCenterAPI.Models.NewsImages.Business;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.News
{
    public class NewsRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public NewsRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<NewsBasicModel>> GetAllAsync()
        {
            string query = @"SELECT newsid, name, type, imagepath, discription, writter, insertdate FROM [news] order by insertdate desc";
            var result = await _db.QueryAsync<NewsBasicModel>(query);
            return result;
        }

        public async Task<NewsBusinessModel> GetByIDAsync(Guid newsID)
        {
            string query = @"Select newsid, name, type, imagepath, discription, firstdetails, seconddetails, writter, insertdate from [news] where [newsid] = @newsID ";
            query += "Select imageid, imagepath from [newsimage] where [newsid] = @newsID";
            using (var multi = await _db.QueryMultipleAsync(query, new { newsID }))
            {
                var result = await multi.ReadFirstOrDefaultAsync<NewsBusinessModel>();
                if (result != null)
                {
                    result.NewsImages = await multi.ReadAsync<NewsImagesBusinessModel>();
                }
                return result;
            }
        }

        public async Task<bool> IsExistByIDAsync(Guid newsID)
        {
            string query = @"Select count(1) from [news] where [newsid] = @newsID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { newsID });
            return result;
        }

        public async Task<Guid> AddAsync(NewsBasicModel model)
        {
            string query = @"INSERT INTO [dbo].[news] ([name] ,[type], [imagepath], [discription], [writter], [firstdetails], [seconddetails] ,[insertdate]) Output Inserted.newsid
                                VALUES (@Name, @Type, @ImagePath, @Discription, @Writter, @FirstDetails, @SecondDetails, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, model);
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(NewsBasicModel model)
        {
            string query = @"UPDATE [news] SET [name] = @name,  [type] = @type, [imagepath] = @imagepath,
                             [discription] = @discription, [writter] = @writter, [firstdetails] = @firstdetails, [seconddetails] = @seconddetails
                             where [newsid] = @newsID";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid newsID)
        {
            string query = "Delete from [news] where [newsid] = @newsID";
            var result = await _db.ExecuteAsync(query, new { newsID });
            return result;
        }
    }
}