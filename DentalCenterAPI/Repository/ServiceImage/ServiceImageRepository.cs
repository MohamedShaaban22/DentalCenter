using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.ServiceImage.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.ServiceImage
{
    public class ServiceImageRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public ServiceImageRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<ServiceImageBasicModel>> GetAllAsync(string serviceName)
        {
            string query = "SELECT * from serviceimages Where LOWER(servicename) = @serviceName";
            var result = await _db.QueryAsync<ServiceImageBasicModel>(query, new { serviceName });
            return result;
        }

        public async Task<bool> IsExistByIDAsync(Guid serviceImageID)
        {
            string query = @"Select count(1) from [serviceimages] where [serviceimagesid] = @serviceImageID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { serviceImageID });
            return result;
        }

        public async Task<int> AddAsync(IEnumerable<ServiceImageBasicModel> entity)
        {
            string query = @"INSERT INTO [dbo].[serviceimages] ([servicename] ,[beforeimagepath], [afterimagepath], [description], [insertdate])
                                VALUES (@servicename, @beforeimagepath, @afterimagepath, @description, @insertdate)";
            var result = await _db.ExecuteAsync(query, entity);
            return result;
        }

        public async Task<int> UpdateAsync(ServiceImageBasicModel model)
        {
            string query = @"UPDATE [serviceimages] SET [servicename] = @servicename, [BeforeImagePath] = @BeforeImagePath, [AfterImagePath] = @BeforeImagePath, [description] = @description
                             where [serviceimagesid] = @serviceImagesID";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid serviceImageID)
        {
            string query = "Delete from [serviceimages] where [serviceimagesid] = @serviceImageID";
            var result = await _db.ExecuteAsync(query, new { serviceImageID });
            return result;
        }

        public async Task<int> DeleteAsync(IEnumerable<Guid> serviceImageIDs)
        {
            string query = "Delete from [serviceimages] where [serviceimagesid] IN @serviceImageIDs";
            var result = await _db.ExecuteAsync(query, new { serviceImageIDs });
            return result;
        }
    }
}