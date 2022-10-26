using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Common;
using DentalCenterAPI.Models.Doctor.Basic;
using DentalCenterAPI.Models.Doctor.Business;
using DentalCenterAPI.Repository.Doctor;

namespace DentalCenterAPI.Services.Doctor
{
    public class DoctorService : IDoctorService
    {
        private DoctorRepository _repo;
        private IMapper _mapper;
        public DoctorService(DoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All Doctors For(Admin, Web)
        /// </summary>
        /// <param name="orderByAsc"></param>
        /// <param name="sortBy">Name, ReferalNumber</param>
        /// <param name="search"></param>
        /// <returns>Return List of Doctors</returns>
        public async Task<IEnumerable<DoctorBasicModel>> GetAllAsync(bool orderByAsc, string sortBy, string search)
        {
            try
            {
                var result = await _repo.GetAllAsync();
                if (!string.IsNullOrEmpty(search))
                    result = result.Where(x => x.Name?.ToLower().Contains(search) == true || x.ReferalNumber?.ToLower().Contains(search) == true);

                switch (sortBy.ToLower())
                {
                    case "name":
                        result = (orderByAsc) ? result.OrderBy(x => x.Name) : result.OrderByDescending(x => x.Name);
                        break;
                    case "referalnumber":
                        result = (orderByAsc) ? result.OrderBy(x => x.ReferalNumber) : result.OrderByDescending(x => x.ReferalNumber);
                        break;
                    default:
                        result = (orderByAsc) ? result.OrderBy(x => x.Name) : result.OrderByDescending(x => x.Name);
                        break;
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
        /// Get All Doctors (NAme,ID) For Dropsown (For Admin)
        /// </summary>
        /// <returns>Return List of Doctors(ID, Name)</returns>
        public async Task<IEnumerable<DataBusinessModel>> GetAllForDropdownAsync()
        {
            try
            {
                var result = await _repo.GetAllForDropdownAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get All Doctors (Name,ID, SocialLinks,First Discription) For for homepage (For Web)
        /// </summary>
        /// <returns>Return List of Doctors(Name,ID, SocialLinks,First Discription)</returns>
        public async Task<IEnumerable<DoctorBasicModel>> GetAllForHomeAsync()
        {
            try
            {
                var result = await _repo.GetAllForHomeAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get All Doctors (Name,ID, Image) For for homepage (For Web)
        /// </summary>
        /// <returns>Return List of Doctors(Name,ID, Image)</returns>
        public async Task<IEnumerable<DoctorBasicModel>> GetAllForSliderAsync()
        {
            try
            {
                var result = await _repo.GetAllForSliderAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Doctor With Patients, Blogs By Doctor ID (for Admin, Web)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return Doctor Details with Patients, Blogs </returns>
        public async Task<DoctorBusinessModel> GetByIDAsync(Guid doctorID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(doctorID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Doctor exist by  ID(for admin)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return True, False</returns>
        public async Task<bool?> IsExistByIDAsync(Guid doctorID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(doctorID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Doctor (for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<Guid?> AddAsync(DoctorBasicModel model)
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
        /// Update Doctor (for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(DoctorBasicModel model)
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
        /// Delete With Patients, Blogs(for admin)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid doctorID)
        {
            try
            {
                var result = await _repo.DeleteAsync(doctorID);
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