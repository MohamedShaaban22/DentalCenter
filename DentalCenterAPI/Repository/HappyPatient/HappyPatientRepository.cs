using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.HappyPatient.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.HappyPatient
{
    public class HappyPatientRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public HappyPatientRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<HappyPatientBasicModel>> GetAllAsync(string type, bool? isFavorite)
        {
            string query = @"SELECT * FROM [happypatient] where LOWER(type) = @type ";
            if (isFavorite != null)
                query += (isFavorite == true) ? @" AND isfavorite = true " : " AND (isfavorite = false or isfavorite is null) ";
            var result = await _db.QueryAsync<HappyPatientBasicModel>(query, new { type });
            return result;
        }

        public async Task<HappyPatientBasicModel> GetByIDAsync(Guid happyPatientID, string type)
        {
            string query = @"SELECT * FROM [happypatient] where LOWER(type) = @type AND [happypatientid] = @happyPatientID";
            var result = await _db.QueryFirstOrDefaultAsync<HappyPatientBasicModel>(query, new { type, happyPatientID });
            return result;
        }

        public async Task<HappyPatientBasicModel> GetRandomByTypeAsync(string type)
        {
            string query = @"select top 1 * from happypatient where LOWER(type) = @type order by NEWID()";
            var result = await _db.QueryFirstOrDefaultAsync<HappyPatientBasicModel>(query, new { type });
            return result;
        }

        public async Task<bool> IsExistByIDAsync(Guid happyPatientID)
        {
            string query = @"Select count(1) from [happypatient] where [happypatientid] = @happyPatientID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { happyPatientID });
            return result;
        }

        public async Task<int> AddAsync(IEnumerable<HappyPatientBasicModel> entity)
        {
            string query = @"INSERT INTO [dbo].[happypatient] ([isfavorite] ,[videopath], [ImagePath], [name], [job], [comment], [type] ,[insertdate])
                                VALUES (@isfavorite, @videopath, @ImagePath, @name, @job, @comment, @type, @insertdate)";
            var result = await _db.ExecuteAsync(query, entity);
            return result;
        }

        public async Task<int> UpdateIsFavoriteAsync(Guid happyPatientID, bool isFavorite)
        {
            string query = @"UPDATE [happypatient] SET [isfavorite] = @isFavorite 
                             where [happypatientid] = @happyPatientID";
            var result = await _db.ExecuteAsync(query, new { happyPatientID, isFavorite });
            return result;
        }

        public async Task<int> UpdateAsync(HappyPatientBasicModel model)
        {
            string query = @"UPDATE [happypatient] SET [isfavorite] = @isFavorite , [VideoPath] = VideoPath, [ImagePath] = @ImagePath ,[name] = @name, [job] = @job
                            , [comment] = @comment, [InsertDate] = @InsertDate
                             where [happypatientid] = @happyPatientID and LOWER(type) = @type";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid happyPatientID)
        {
            string query = "Delete from [happypatient] where [happypatientid] = @happyPatientID";
            var result = await _db.ExecuteAsync(query, new { happyPatientID });
            return result;
        }
    }
}