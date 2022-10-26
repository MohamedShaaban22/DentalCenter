using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Branch.Basic;
using DentalCenterAPI.Models.Common;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Branch
{
    public class BranchRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public BranchRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<BranchBasicModel>> GetAllAsync()
        {
            string query = "SELECT * from branch order by name";
            var result = await _db.QueryAsync<BranchBasicModel>(query);
            return result;
        }

        public async Task<BranchBasicModel> GetByIDAsync(Guid branchID)
        {
            string query = "Select * from branch Where branchid = @branchID";
            var result = await _db.QueryAsync<BranchBasicModel>(query, new { branchID });
            return result.FirstOrDefault();
        }

        public async Task<bool> IsExistByIDAsync(Guid branchID)
        {
            string query = @"Select count(1) from [branch] where [branchid] = @branchID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { branchID });
            return result;
        }

        public async Task<Guid> AddAsync(BranchBasicModel entity)
        {
            string query = @"INSERT INTO [dbo].[branch] ([ismain] ,[name] ,[imagepath] ,[address] ,[fromday] ,[today] ,[fromhour]
                                    ,[tohour] ,[phonenumber] ,[textnumber] ,[insertdate]) Output Inserted.[branchid]
                                VALUES (@ismain, @name, @imagepath, @address, @fromday, @today, @fromhour, @tohour, @phonenumber, @textnumber, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, entity);
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(BranchBasicModel entity)
        {
            string query = @"UPDATE [branch] SET [name] = @name , [ismain] = @ismain, [imagepath] = @imagepath,
                             [address]= @address, [fromday]= @fromday, [today]= @today, [fromhour]= @fromhour, [tohour]= @tohour, [phonenumber]= @phonenumber,
                             [textnumber]= @textnumber, [insertdate]= @insertdate
                             WHERE [branchid] = @branchid";
            var result = await _db.ExecuteAsync(query, entity);
            return result;
        }

        public async Task<int> DeleteAsync(Guid branchID)
        {
            string query = "Delete from [branch] where [branchid] = @branchID";
            var result = await _db.ExecuteAsync(query, new { branchID });
            return result;
        }
    }
}