using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Blog.Basic;
using DentalCenterAPI.Models.Blog.Business;
using DentalCenterAPI.Models.BlogDetails.Business;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Blog
{
    public class BlogRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public BlogRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<BlogBasicModel>> GetAllAsync()
        {
            string query = @"SELECT * FROM [blog] order by insertdate desc";
            var result = await _db.QueryAsync<BlogBasicModel>(query);
            return result;
        }

        public async Task<BlogBusinessModel> GetByIDAsync(Guid blogID)
        {
            string query = @"Select * from [blog] where [blogid] = @blogID ";
            query += "Select * from [blogdetails] where [blogid] = @blogID order by detailsorder asc ";
            using (var multi = await _db.QueryMultipleAsync(query, new { blogID }))
            {
                var result = await multi.ReadFirstOrDefaultAsync<BlogBusinessModel>();
                if (result != null)
                {
                    result.BlogDetails = await multi.ReadAsync<BlogDetailsBusinessModel>();
                }
                return result;
            }
        }

        public async Task<bool> IsExistByIDAsync(Guid blogID)
        {
            string query = @"Select count(1) from [blog] where [blogid] = @blogID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { blogID });
            return result;
        }

        public async Task<Guid> AddAsync(BlogBasicModel model)
        {
            string query = @"INSERT INTO [dbo].[blog] ([title], [writter], [imagepath],[doctorid] ,[insertdate]) Output Inserted.blogid
                                VALUES (@title, @writter, @imagepath,@doctorid, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, model);
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(BlogBasicModel model)
        {
            string query = @"UPDATE [blog] SET [title] = @title, [writter] = @writter, [insertdate] = @insertdate, [imagepath] = @imagepath, [doctorid] = @doctorid
                             where [blogid] = @blogID";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid blogID)
        {
            string query = "Delete from [blog] where [blogid] = @blogID";
            var result = await _db.ExecuteAsync(query, new { blogID });
            return result;
        }
    }
}