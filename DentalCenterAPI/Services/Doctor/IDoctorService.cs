using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Common;
using DentalCenterAPI.Models.Doctor.Basic;
using DentalCenterAPI.Models.Doctor.Business;

namespace DentalCenterAPI.Services.Doctor
{
    public interface IDoctorService
    {
        /// <summary>
        /// Get All Doctors For(Admin, Web)
        /// </summary>
        /// <param name="orderByAsc"></param>
        /// <param name="sortBy">Name, ReferalNumber</param>
        /// <param name="search"></param>
        /// <returns>Return List of Doctors</returns>
        Task<IEnumerable<DoctorBasicModel>> GetAllAsync(bool orderByAsc, string sortBy, string search);

        /// <summary>
        /// Get All Doctors (NAme,ID) For Dropsown (For Admin)
        /// </summary>
        /// <returns>Return List of Doctors(ID, Name)</returns>
        Task<IEnumerable<DataBusinessModel>> GetAllForDropdownAsync();

        /// <summary>
        /// Get All Doctors (Name,ID, SocialLinks,First Discription) For for homepage (For Web)
        /// </summary>
        /// <returns>Return List of Doctors(Name,ID, SocialLinks,First Discription)</returns>
        Task<IEnumerable<DoctorBasicModel>> GetAllForHomeAsync();


        /// <summary>
        /// Get All Doctors (Name,ID, Image) For for homepage (For Web)
        /// </summary>
        /// <returns>Return List of Doctors(Name,ID, Image)</returns>
        Task<IEnumerable<DoctorBasicModel>> GetAllForSliderAsync();

        /// <summary>
        /// Get Doctor With Patients, Blogs By Doctor ID (for Admin, Web)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return Doctor Details with Patients, Blogs </returns>
        Task<DoctorBusinessModel> GetByIDAsync(Guid doctorID);

        /// <summary>
        /// Check Doctor exist by  ID(for admin)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return True, False</returns>
        Task<bool?> IsExistByIDAsync(Guid doctorID);

        /// <summary>
        /// Add Doctor (for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<Guid?> AddAsync(DoctorBasicModel model);

        /// <summary>
        /// Update Doctor (for admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(DoctorBasicModel model);

        /// <summary>
        /// Delete With Patients, Blogs(for admin)
        /// </summary>
        /// <param name="doctorID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid doctorID);
    }
}