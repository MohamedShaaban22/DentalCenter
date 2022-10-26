using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Appointment.Business;

namespace DentalCenterAPI.Services.Appointment
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Get All Appointments With Patient Details By Type, IsNew With Search (For Admin)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="search">BranchName, ServiceName, PhoneNumber, PatientName</param>
        /// <returns>Return List of appointment Details</returns>
        Task<IEnumerable<AppointmentBusinessModel>> GetAllByTypeAsync(string type, string search);

        /// <summary>
        /// Get Appointment With Patient Details By appointmentID (For Admin)
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns>Return appointment Details</returns>
        Task<AppointmentBusinessModel> GetByIDAsync(Guid appointmentID);

        /// <summary>
        /// Add Appointment With Patient (For Web)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Row GUID</returns>
        Task<Guid?> AddAsync(AppointmentAddBusinessModel model);

        /// <summary>
        /// Delete Appointment With Patient Details (For Admin)
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid appointmentID);
    }
}