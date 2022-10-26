using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.DoctorPatient.Basic;

namespace DentalCenterAPI.Services.DoctorPatient
{
    public interface IDoctorPatientService
    {
        /// <summary>
        /// Get All Doctors Patients For(Admin)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return List of Doctor Patients</returns>
        Task<IEnumerable<DoctorPatientBasicModel>> GetAllAsync(Guid doctorID);

        /// <summary>
        /// Add Doctor Patients(for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        Task<Guid?> AddAsync(DoctorPatientBasicModel model);

        /// <summary>
        /// Update Doctor Patient(for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(DoctorPatientBasicModel model);

        /// <summary>
        /// Delete Patient By doctorPatientID(for admin)
        /// </summary>
        /// <param name="doctorPatientID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid doctorPatientID);
    }
}