using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Subscribtion.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Subscribtion
{
    public class SubscribtionRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public SubscribtionRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<SubscribtionBasicModel>> GetAllAsync()
        {
            string query = "SELECT * from subscribtion order by email";
            var result = await _db.QueryAsync<SubscribtionBasicModel>(query);
            return result;
        }

        public async Task<bool> IsExistByIDAsync(Guid subscribtionID)
        {
            string query = @"Select count(1) from [subscribtion] where [subscribtionid] = @subscribtionID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { subscribtionID });
            return result;
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            string query = @"Select count(1) from [subscribtion] where LOWER([email]) = @email";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { email });
            return result;
        }

        public async Task<Guid> AddAsync(SubscribtionBasicModel entity)
        {
            string query = @"INSERT INTO [dbo].[subscribtion] ([email] ,[insertdate]) Output Inserted.[subscribtionid]
                                VALUES (@email, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, entity);
            return result.FirstOrDefault();
        }

        public async Task<int> DeleteAsync(Guid subscribtionID)
        {
            string query = "Delete from [subscribtion] where [subscribtionid] = @subscribtionID";
            var result = await _db.ExecuteAsync(query, new { subscribtionID });
            return result;
        }
    }
}