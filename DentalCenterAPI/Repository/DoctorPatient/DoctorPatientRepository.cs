using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.DoctorPatient.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.DoctorPatient
{
    public class DoctorPatientRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public DoctorPatientRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<DoctorPatientBasicModel>> GetAllByDoctorIDAsync(Guid doctorID)
        {
            string query = "SELECT * from doctorpatient Where doctorid = @doctorID";
            var result = await _db.QueryAsync<DoctorPatientBasicModel>(query, new { doctorID });
            return result;
        }

        public async Task<Guid> AddAsync(DoctorPatientBasicModel model)
        {
            string query = @"Insert Into doctorpatient (patientwithdoctorimagepath, patientimagepath, caught, doctorid, insertdate) output inserted.doctorpatientid 
                            Values (@patientwithdoctorimagepath, @patientimagepath, @caught, @doctorid, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, model);
            return result.FirstOrDefault(); ;
        }

        public async Task<int> UpdateAsync(DoctorPatientBasicModel model)
        {
            string query = @"UPDATE [doctorpatient] SET [patientwithdoctorimagepath] = @patientwithdoctorimagepath, [patientimagepath] = @patientimagepath, [caught] = @caught, [doctorid] = @doctorid,
                                                 [insertdate] = @insertdate     
                            where [doctorpatientid] = @doctorpatientid";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid doctorPatientID)
        {
            string query = "Delete from [doctorpatient] where [doctorpatientid] = @doctorpatientid";
            var result = await _db.ExecuteAsync(query, new { doctorPatientID });
            return result;
        }
    }
}