using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Admin.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Admin
{
    public class AdminRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public AdminRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<AdminBasicModel> LoginAsync(string emailOrName, string password)
        {
            string query = @"Select * from [admin] where (email = @emailOrName OR username = @emailOrName) AND password = @password";
            var result = await _db.QueryAsync<AdminBasicModel>(query, new { emailOrName, password });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<AdminBasicModel>> GetAllAsync()
        {
            string query = @"SELECT adminid, username, email FROM [admin] Order By username";
            var result = await _db.QueryAsync<AdminBasicModel>(query);
            return result;
        }

        public async Task<AdminBasicModel> GetByIDAsync(Guid adminID)
        {
            string query = @"SELECT * FROM [admin] WHERE [adminid] = @adminID";
            var result = await _db.QueryAsync<AdminBasicModel>(query, new { adminID });
            return result.FirstOrDefault();
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            string query = @"Select count(1) from [admin] where [email] = @email";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { email });
            return result;
        }

        public async Task<bool> IsUserNameExistAsync(string userName)
        {
            string query = @"Select count(1) from [admin] where [username] = @userName";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { userName });
            return result;
        }

        public async Task<bool> IsEmailExistAsync(string email, Guid currentAdminID)
        {
            string query = @"Select count(1) from [admin] where [email] = @email AND adminid != @currentAdminID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { email, currentAdminID });
            return result;
        }

        public async Task<bool> IsUserNameExistAsync(string userName, Guid currentAdminID)
        {
            string query = @"Select count(1) from [admin] where [username] = @userName AND adminid != @currentAdminID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { userName, currentAdminID });
            return result;
        }

        public async Task<bool> IsExistByIDAsync(Guid adminID)
        {
            string query = @"Select count(1) from [admin] where [adminid] = @adminID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { adminID });
            return result;
        }

        public async Task<Guid> AddAsync(AdminBasicModel entity)
        {
            string query = @"INSERT INTO [admin] ([username], [email], [password], [insertdate]) output inserted.adminid 
                            VALUES (@username, @email, @password, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, entity);
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(AdminBasicModel entity)
        {
            string query = @"UPDATE [admin] SET [username] = @username, [email] = @email, [insertdate] = @insertdate";
            if (!string.IsNullOrEmpty(entity.Password))
                query = query + ", [password] = @password where [adminid] = @adminid";
            else
                query = query + " where [adminid] = @adminid";
            var result = await _db.ExecuteAsync(query, entity);
            return result;
        }

        public async Task<int> DeleteAsync(Guid adminID)
        {
            string query = "Delete from [admin] where [adminid] = @adminID";
            var result = await _db.ExecuteAsync(query, new { adminID });
            return result;
        }
    }
}