using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Service.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Service
{
    public class ServiceRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public ServiceRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<ServiceBasicModel>> GetAllAsync()
        {
            string query = "SELECT serviceid, name, info from [service] order by name";
            var result = await _db.QueryAsync<ServiceBasicModel>(query);
            return result;
        }

        public async Task<ServiceBasicModel> GetByIDAsync(Guid serviceID)
        {
            string query = "SELECT * from [service] where [serviceID] = @serviceID";
            var result = await _db.QueryFirstOrDefaultAsync<ServiceBasicModel>(query, new { serviceID });
            return result;
        }

        public async Task<ServiceBasicModel> GetByNameAsync(string name)
        {
            string query = "SELECT * from [service] where LOWER(name) = @name";
            var result = await _db.QueryFirstOrDefaultAsync<ServiceBasicModel>(query, new { name });
            return result;
        }

        public async Task<bool> IsExistByIDAsync(Guid serviceID)
        {
            string query = @"Select count(1) from [service] where [serviceID] = @serviceID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { serviceID });
            return result;
        }

        public async Task<bool> IsExistByNameAsync(string name)
        {
            string query = @"Select count(1) from [service] where LOWER([name]) = @name";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { name });
            return result;
        }

        public async Task<bool> IsExistByNameAsync(Guid serviceID, string name)
        {
            string query = @"Select count(1) from [service] where LOWER([name]) = @name AND serviceID != @serviceID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { name, serviceID });
            return result;
        }

        public async Task<Guid> AddAsync(ServiceBasicModel entity)
        {
            string query = @"INSERT INTO [dbo].[service] ([name] ,[info], [VideoPath], [infopartone], [infoparttwo], [insertdate]) output inserted.serviceid 
                                VALUES (@name, @info, @VideoPath, @infopartone, @infoparttwo, @insertdate)";
            var result = await _db.QueryFirstOrDefaultAsync<Guid>(query, entity);
            return result;
        }

        public async Task<int> UpdateAsync(ServiceBasicModel model)
        {
            string query = @"UPDATE [service] SET [name] = @name, [info] = @info, [VideoPath] = @VideoPath, [infopartone] = @infopartone
                            , [infoparttwo] = @infoparttwo
                             where [serviceID] = @serviceID";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid serviceID)
        {
            string query = "Delete from [service] where [serviceID] = @serviceID";
            var result = await _db.ExecuteAsync(query, new { serviceID });
            return result;
        }
    }
}