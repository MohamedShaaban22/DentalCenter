using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.FamousSection.Basic;
using DentalCenterAPI.Models.FamousSection.Business;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.FamousSection
{
    public class FamousSectionRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public FamousSectionRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<FamousSectionBasicModel>> GetAllAsync()
        {
            string query = @"SELECT * FROM [famoussection] order by name";
            var result = await _db.QueryAsync<FamousSectionBasicModel>(query);
            return result;
        }

        public async Task<FamousSectionBusinessModel> GetByIDAsync(Guid famousSectionID)
        {
            string query = @"Select * from [famoussection] where [famoussectionid] = @famousSectionID ";
            query += "Select imagepath from [famoussectionImage] where [famoussectionid] = @famousSectionID";
            using (var multi = await _db.QueryMultipleAsync(query, new { famousSectionID }))
            {
                var result = await multi.ReadFirstOrDefaultAsync<FamousSectionBusinessModel>();
                if (result != null)
                {
                    result.FamousSectionImages = await multi.ReadAsync<FamousSectionImageBusinessModel>();
                }
                return result;
            }
        }

        public async Task<bool> IsExistByIDAsync(Guid famousSectionID)
        {
            string query = @"Select count(1) from [famoussection] where [famousSectionID] = @famousSectionID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { famousSectionID });
            return result;
        }

        public async Task<Guid> AddAsync(FamousSectionBasicModel model)
        {
            string query = @"INSERT INTO [dbo].[famoussection] ([name] , [imagepath] ,[insertdate]) Output Inserted.famoussectionid
                                VALUES (@Name, @ImagePath, @insertdate)";
            var result = await _db.QueryFirstOrDefaultAsync<Guid>(query, model);
            return result;
        }

        public async Task<int> UpdateAsync(FamousSectionBasicModel model)
        {
            string query = @"UPDATE [famoussection] SET [name] = @name, [imagepath] = @imagepath, [insertdate] = @insertdate
                             where [famoussectionid] = @famoussectionid";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid famousSectionID)
        {
            string query = "Delete from [famoussection] where [famoussectionid] = @famousSectionID";
            var result = await _db.ExecuteAsync(query, new { famousSectionID });
            return result;
        }
    }
}