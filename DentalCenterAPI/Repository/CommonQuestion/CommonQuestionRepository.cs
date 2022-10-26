using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.CommonQuestion.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.CommonQuestion
{
    public class CommonQuestionRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public CommonQuestionRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<CommonQuestionBasicModel>> GetAllAsync(string type)
        {
            string query = "SELECT * from commonquestion ";
            if (!string.IsNullOrEmpty(type) && type.ToLower() != "all")
                query += @"Where LOWER(type) = LOWER(@type) ";
            query += @" order by insertdate desc";
            var result = await _db.QueryAsync<CommonQuestionBasicModel>(query, new {type});
            return result;
        }

        public async Task<CommonQuestionBasicModel> GetByIDAsync(Guid commonQuestionID)
        {
            string query = "Select * from commonquestion Where commonquestionid = @commonQuestionID";
            var result = await _db.QueryAsync<CommonQuestionBasicModel>(query, new { commonQuestionID });
            return result.FirstOrDefault();
        }

        public async Task<bool> IsExistByIDAsync(Guid commonQuestionID)
        {
            string query = @"Select count(1) from [commonquestion] where [commonquestionid] = @commonQuestionID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { commonQuestionID });
            return result;
        }

        public async Task<Guid> AddAsync(CommonQuestionBasicModel entity)
        {
            string query = @"INSERT INTO [dbo].[commonquestion] ([question] ,[answer], [type], [insertdate]) Output Inserted.[commonquestionid]
                                VALUES (@question, @answer, @type, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, entity);
            return result.FirstOrDefault();
        }

        public async Task<int> UpdateAsync(CommonQuestionBasicModel entity)
        {
            string query = @"UPDATE [commonquestion] SET [question] = @question , [answer] = @answer, [type] = @type, [insertdate]= @insertdate
                             WHERE [commonquestionid] = @commonQuestionID";
            var result = await _db.ExecuteAsync(query, entity);
            return result;
        }

        public async Task<int> DeleteAsync(Guid commonQuestionID)
        {
            string query = "Delete from [commonquestion] where [commonquestionid] = @commonQuestionID";
            var result = await _db.ExecuteAsync(query, new { commonQuestionID });
            return result;
        }
    }
}