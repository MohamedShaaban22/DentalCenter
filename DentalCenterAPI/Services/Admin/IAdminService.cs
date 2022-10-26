using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Admin.Basic;
using DentalCenterAPI.Models.Admin.Business;

namespace DentalCenterAPI.Services.Admin
{
    public interface IAdminService
    {
        /// <summary>
        /// Admin Login By ( email Or username ) and Password (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Logged User Token</returns>
        Task<UserLoginBusinessModel> LoginAsync(UserCredentialsBusinessModel model);

        /// <summary>
        /// Get List of Admins Details By UserID (For Admin)
        /// </summary>
        /// <returns>Return List of Admins Details</returns>
        Task<IEnumerable<AdminBasicModel>> GetAllAsync();

        /// <summary>
        /// Get Admin Details By UserID (For Admin)
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns>Return Admin Details</returns>
        Task<AdminBasicModel> GetByIDAsync(Guid adminID);

        /// <summary>
        /// Check UserName Existance (For Admin)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsUserNameExistAsync(string userName);

        /// <summary>
        /// Check UserName Existance and exclude current admin (For Admin)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="excludedAdminID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsUserNameExistAsync(string userName, Guid excludedAdminID);

        /// <summary>
        /// Check Email Existance (For Admin)
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsEmailExistAsync(string email);

        /// <summary>
        /// Check Email Existance and exclude current admin (For Admin)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="excludedAdminID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsEmailExistAsync(string email, Guid excludedAdminID);

        /// <summary>
        /// Check Admin Existance (For Admin)
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid adminID);

        /// <summary>
        /// Add Admin  (For Admin)
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>Return Added Admin GUID</returns>
        Task<Guid?> AddAsync(AdminBasicModel admin);

        /// <summary>
        /// Update Admin  (For Admin)
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(AdminBasicModel admin);

        /// <summary>
        /// Delete Admin  (For Admin)
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid adminID);
    }
}