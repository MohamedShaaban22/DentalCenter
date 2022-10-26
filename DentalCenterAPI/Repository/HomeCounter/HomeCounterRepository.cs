using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.HomeCounter.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.HomeCounter
{
    public class HomeCounterRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public HomeCounterRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<HomeCounterBasicModel> GetFirstAsync()
        {
            string query = "SELECT Top 1 * from homecounter";
            var result = await _db.QueryAsync<HomeCounterBasicModel>(query);
            return result.FirstOrDefault();
        }

        public async Task<bool> IsExistByIDAsync(Guid homeCounterID)
        {
            string query = @"Select count(1) from [homecounter] where [homecounterid] = @homeCounterID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { homeCounterID });
            return result;
        }

        public async Task<Guid> AddAsync(HomeCounterBasicModel model)
        {
            string query = @"Insert Into homecounter (doctorscount, happypatientscount, branchescount, expyearscount, insertdate) output inserted.homecounterid 
                            Values (@doctorscount, @happypatientscount, @branchescount, @expyearscount, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, model);
            return result.FirstOrDefault(); ;
        }

        public async Task<int> UpdateAsync(HomeCounterBasicModel model)
        {
            string query = @"UPDATE [homecounter] SET [doctorscount] = @doctorscount,  [happypatientscount] = @happypatientscount, [branchescount] = @branchescount,
                            [expyearscount] = @expyearscount, [insertdate] = @insertdate     
                            where [homecounterid] = @homeCounterID";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid homeCounterID)
        {
            string query = "Delete from [homecounter] where [homecounterid] = @homeCounterID";
            var result = await _db.ExecuteAsync(query, new { homeCounterID });
            return result;
        }
    }
}