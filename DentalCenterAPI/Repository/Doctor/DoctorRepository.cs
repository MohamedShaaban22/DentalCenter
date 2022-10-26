using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Blog.Basic;
using DentalCenterAPI.Models.Common;
using DentalCenterAPI.Models.Doctor.Basic;
using DentalCenterAPI.Models.Doctor.Business;
using DentalCenterAPI.Models.DoctorPatient.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Doctor
{
    public class DoctorRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public DoctorRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<DoctorBasicModel>> GetAllAsync()
        {
            string query = "SELECT * from doctor order by name";
            var result = await _db.QueryAsync<DoctorBasicModel>(query);
            return result;
        }

        public async Task<IEnumerable<DataBusinessModel>> GetAllForDropdownAsync()
        {
            string query = "SELECT doctorid as id, name from doctor order by name";
            var result = await _db.QueryAsync<DataBusinessModel>(query);
            return result;
        }

        public async Task<IEnumerable<DoctorBasicModel>> GetAllForHomeAsync()
        {
            string query = @"SELECT doctorid, name, homeimagepath, firstdiscription, tiktokurl, facebookurl, instgramurl, linkedinurl 
                from doctor where HomeDisplay = 1 order by name";
            var result = await _db.QueryAsync<DoctorBasicModel>(query);
            return result;
        }

        public async Task<IEnumerable<DoctorBasicModel>> GetAllForSliderAsync()
        {
            string query = "SELECT doctorid, name, exploreimagepath from doctor order by name";
            var result = await _db.QueryAsync<DoctorBasicModel>(query);
            return result;
        }

        public async Task<DoctorBusinessModel> GetByIDAsync(Guid doctorID)
        {
            string query = @"SELECT * from doctor Where doctorid = @doctorID 

             SELECT * from doctorpatient Where doctorid = @doctorID
              SELECT * from Blog Where doctorid = @doctorID order by insertdate desc";
            using (var multi = await _db.QueryMultipleAsync(query, new { doctorID }))
            {
                var result = await multi.ReadFirstOrDefaultAsync<DoctorBusinessModel>();
                if (result != null)
                {
                    result.DoctorPatients = await multi.ReadAsync<DoctorPatientBasicModel>();
                    result.DoctorBlogs = await multi.ReadAsync<BlogBasicModel>();
                }
                return result;
            }
        }

        public async Task<bool> IsExistByIDAsync(Guid doctorID)
        {
            string query = @"Select count(1) from [doctor] where [doctorid] = @doctorID";
            var result = await _db.ExecuteScalarAsync<bool>(query, new { doctorID });
            return result;
        }


        public async Task<Guid> AddAsync(DoctorBasicModel model)
        {
            string query = @"Insert Into doctor (name, firstdiscription, seconddiscription, thirddiscription, referalnumber, homeimagepath, personalimagepath, exploreimagepath 
                                                , videopath, tiktokurl, facebookurl, instgramurl, linkedinurl, homedisplay, insertdate) output inserted.doctorid 
                            Values (@name, @firstdiscription, @seconddiscription, @thirddiscription, @referalnumber, @homeimagepath, @personalimagepath, @exploreimagepath,
                             @videopath, @tiktokurl, @facebookurl, @instgramurl, @linkedinurl, @homedisplay, @insertdate)";
            var result = await _db.QueryAsync<Guid>(query, model);
            return result.FirstOrDefault(); ;
        }

        public async Task<int> UpdateAsync(DoctorBasicModel model)
        {
            string query = @"UPDATE [doctor] SET [name] = @name, [firstdiscription] = @firstdiscription, [seconddiscription] = @seconddiscription, [thirddiscription] = @thirddiscription,
                                                 [referalnumber] = @referalnumber, [videopath] = @videopath, [homeimagepath] = @homeimagepath,[personalimagepath] = @personalimagepath,[exploreimagepath] = @exploreimagepath,
                                                 [tiktokurl] = @tiktokurl, [facebookurl] = @facebookurl, [instgramurl] = @instgramurl, 
                                                 [linkedinurl] = @linkedinurl, [homedisplay] = @homedisplay, [insertdate] = @insertdate     
                            where [doctorid] = @doctorID";
            var result = await _db.ExecuteAsync(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid doctorID)
        {
            string query = "Delete from [doctor] where [doctorid] = @doctorID";
            var result = await _db.ExecuteAsync(query, new { doctorID });
            return result;
        }
    }
}