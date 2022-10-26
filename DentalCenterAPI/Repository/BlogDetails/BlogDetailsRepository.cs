using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DentalCenterAPI.Models.BlogDetails.Basic;
using Dapper;

namespace DentalCenterAPI.Repository.BlogDetails
{
    public class BlogDetailsRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public BlogDetailsRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<int> AddAsync(IEnumerable<BlogDetailsBasicModel> model)
        {
            string query = @"INSERT INTO [dbo].[blogdetails] ([title], [discreption], [imagepath], [detailsorder], [blogid], [insertdate])
                                VALUES (@title, @discreption, @imagepath, @detailsorder, @blogid, @insertdate)";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> UpdateAsync(IEnumerable<BlogDetailsBasicModel> model)
        {
            string query = @"UPDATE [blogdetails] SET [title] = @title, [discreption] = @discreption, [imagepath] = @imagepath, [detailsorder] = @detailsorder, [insertdate] = @insertdate
                             where [detailsid] = @detailsID And [blogid] = @blogID";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }
    }
}