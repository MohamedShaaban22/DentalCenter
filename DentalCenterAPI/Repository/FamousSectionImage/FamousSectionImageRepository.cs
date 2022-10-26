using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.FamousSection.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.FamousSectionImage
{
    public class FamousSectionImageRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public FamousSectionImageRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<int> AddAsync(IEnumerable<FamousSectionImageBasicModel> model)
        {
            string query = @"INSERT INTO [dbo].[famoussectionimage] ([imagepath], [famoussectionid], [insertdate]) 
                                VALUES (@imagepath, @famoussectionid, @insertdate)";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteByFacmousSectionImageIDAsync(Guid facmousSectionImageID)
        {
            string query = "Delete from [famoussectionimage] where [facmoussectionimageid] = @facmousSectionImageID";
            var result = await _db.ExecuteAsync(query, new { facmousSectionImageID });
            return result;
        }

        public async Task<int> DeleteByFamousSectionIDAsync(Guid facmousSectionID)
        {
            string query = "Delete from [famoussectionimage] where [famoussectionid] = @facmousSectionID";
            var result = await _db.ExecuteAsync(query, new { facmousSectionID });
            return result;
        }
    }
}