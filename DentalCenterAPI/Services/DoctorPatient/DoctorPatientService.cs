using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.DoctorPatient.Basic;
using DentalCenterAPI.Repository.DoctorPatient;

namespace DentalCenterAPI.Services.DoctorPatient
{
    public class DoctorPatientService : IDoctorPatientService
    {
        private DoctorPatientRepository _repo;
        private IMapper _mapper;
        public DoctorPatientService(DoctorPatientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Doctors Patients For(Admin)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return List of Doctor Patients</returns>
        public async Task<IEnumerable<DoctorPatientBasicModel>> GetAllAsync(Guid doctorID)
        {
            try
            {
                var result = await _repo.GetAllByDoctorIDAsync(doctorID);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Doctor Patients(for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        public async Task<Guid?> AddAsync(DoctorPatientBasicModel model)
        {
            try
            {
                model.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.AddAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Doctor Patient(for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(DoctorPatientBasicModel model)
        {
            try
            {
                model.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.UpdateAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }

        /// <summary>
        /// Delete Patient By doctorPatientID(for admin)
        /// </summary>
        /// <param name="doctorPatientID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid doctorPatientID)
        {
            try
            {
                var result = await _repo.DeleteAsync(doctorPatientID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }
    }
}