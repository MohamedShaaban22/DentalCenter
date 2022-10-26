using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Admin.Basic;
using DentalCenterAPI.Models.Admin.Business;
using DentalCenterAPI.Repository.Admin;
using Microsoft.IdentityModel.Tokens;

namespace DentalCenterAPI.Services.Admin
{
    public class AdminService : IAdminService
    {
        private AdminRepository _repo;
        public AdminService(AdminRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Admin Login By ( email Or username ) and Password (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Logged User Token</returns>
        public async Task<UserLoginBusinessModel> LoginAsync(UserCredentialsBusinessModel model)
        {
            try
            {
                //Login Admin 
                var admin = await _repo.LoginAsync(model.EmailOrUserName, model.Password);
                if (admin == null) return null;

                //Generate User Token 
                var adminToken = GenerateToken(admin);
                return new UserLoginBusinessModel() { Token = adminToken.ToString() };
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get List of Admins Details (For Admin)
        /// </summary>
        /// <returns>Return List of Admins Details</returns>
        public async Task<IEnumerable<AdminBasicModel>> GetAllAsync()
        {
            try
            {
                var result = await _repo.GetAllAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Admin Details By UserID (For Admin)
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns>Return Admin Details</returns>
        public async Task<AdminBasicModel> GetByIDAsync(Guid adminID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(adminID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check UserName Existance (For Admin)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsUserNameExistAsync(string userName)
        {
            try
            {
                var result = await _repo.IsUserNameExistAsync(userName);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check UserName Existance and exclude current admin (For Admin)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="excludedAdminID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsUserNameExistAsync(string userName, Guid excludedAdminID)
        {
            try
            {
                var result = await _repo.IsUserNameExistAsync(userName, excludedAdminID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Email Existance (For Admin)
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsEmailExistAsync(string email)
        {
            try
            {
                var result = await _repo.IsEmailExistAsync(email);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Email Existance and exclude current admin (For Admin)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="excludedAdminID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsEmailExistAsync(string email, Guid excludedAdminID)
        {
            try
            {
                var result = await _repo.IsEmailExistAsync(email, excludedAdminID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Admin Existance (For Admin)
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid adminID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(adminID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Admin  (For Admin)
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>Return Added Admin GUID</returns>
        public async Task<Guid?> AddAsync(AdminBasicModel admin)
        {
            try
            {
                admin.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.AddAsync(admin);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Admin  (For Admin)
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(AdminBasicModel admin)
        {
            try
            {
                admin.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.UpdateAsync(admin);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Delete Admin  (For Admin)
        /// </summary>
        /// <param name="adminID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid adminID)
        {
            try
            {
                var result = await _repo.DeleteAsync(adminID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        ///  Generate Token with (UserID, UserType = admin)
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns>Rerurn Generated Token</returns>
        private object GenerateToken(AdminBasicModel identityUser)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Utility.Utility.GetJWTTokenSecretKey());
                var expireTime = Convert.ToInt32(Utility.Utility.GetJWTTokenExpireTime());
                ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim (ClaimTypes.NameIdentifier, identityUser.AdminID.ToString ()),
                    new Claim ("UserType", "admin"),
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddYears(expireTime),
                    SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                    Issuer = Utility.Utility.GetJWTTokenIssuer()
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var result = tokenHandler.WriteToken(token);
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