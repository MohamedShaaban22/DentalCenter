using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Appointment.Business;
using DentalCenterAPI.Repository.Appointment;

namespace DentalCenterAPI.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private AppointmentRepository _repo;
        public AppointmentService(AppointmentRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get All Appointments With Patient Details By Type, IsNew With Search (For Admin)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="search">BranchName, ServiceName, PhoneNumber, PatientName</param>
        /// <returns>Return List of appointment Details</returns>
        public async Task<IEnumerable<AppointmentBusinessModel>> GetAllByTypeAsync(string type, string search)
        {
            try
            {
                var result = await _repo.GetAllByTypeAsync(type.ToLower());
                if (!string.IsNullOrEmpty(search))
                {
                    result = result.Where(x => x.BranchName?.ToLower().Contains(search) == true || x.ServiceName?.ToLower().Contains(search) == true ||
                    x.Patient?.PhoneNumber?.ToLower().Contains(search) == true || x.Patient?.Name?.ToLower().Contains(search) == true).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Appointment With Patient Details By appointmentID (For Admin)
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns>Return appointment Details</returns>
        public async Task<AppointmentBusinessModel> GetByIDAsync(Guid appointmentID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(appointmentID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Appointment With Patient (For Web)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Row GUID</returns>
        public async Task<Guid?> AddAsync(AppointmentAddBusinessModel model)
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
        /// Delete Appointment With Patient Details (For Admin)
        /// </summary>
        /// <param name="appointmentID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid appointmentID)
        {
            try
            {
                var result = await _repo.DeleteAsync(appointmentID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }
    }
}