using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Admin.Basic;
using DentalCenterAPI.Models.Admin.Business;
using DentalCenterAPI.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.AdminUserController
{
    [Authorize]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private IAdminService _service;
        public AdminUserController(IAdminService service)
        {
            _service = service;
        }

        /// <summary>
        ///  User Login by (email Or username) and password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Token contains(userID, userType)</returns>
        [AllowAnonymous]
        [HttpPost("admin/v1/admin/login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsBusinessModel model)
        {
            try
            {
                var result = await _service.LoginAsync(model);
                if (result == null || result.Token == "0")
                    return BadRequest("Invalid Email or UserName or Password");
                return Ok(result);
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
        [HttpGet("admin/v1/admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
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
        /// <param name="adminid"></param>
        /// <returns>Return Admin Details</returns>
        [HttpGet("admin/v1/admin/{adminid}")]
        public async Task<IActionResult> GetByID(Guid adminid)
        {
            try
            {
                var result = await _service.GetByIDAsync(adminid);
                if (result == null)
                    return BadRequest("Failed to get Admin");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>Return Added Admin GUID</returns>
        [HttpPost("admin/v1/admin")]
        public async Task<IActionResult> Add([FromBody] AdminBasicModel admin)
        {
            try
            {
                //Check Email Existance 
                var isEmailExist = await _service.IsEmailExistAsync(admin.Email);
                if (isEmailExist == null || isEmailExist == true)
                    return BadRequest("Email Already Exist");

                //Check Username Existance 
                var isUserNameExist = await _service.IsUserNameExistAsync(admin.UserName);
                if (isUserNameExist == null || isUserNameExist == true)
                    return BadRequest("UserName Already Exist");

                var result = await _service.AddAsync(admin);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add Admin");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Admin
        /// </summary>
        /// <param name="adminid"></param>
        /// <param name="admin"></param>
        /// <returns>Return Updated rows count</returns>
        [HttpPut("admin/v1/admin/{adminid}")]
        public async Task<IActionResult> Update(Guid adminid, [FromBody] AdminBasicModel admin)
        {
            try
            {
                //Check Email Existance 
                var isEmailExist = await _service.IsEmailExistAsync(admin.Email, adminid);
                if (isEmailExist == null || isEmailExist == true)
                    return BadRequest("Email Already Exist");

                //Check Username Existance 
                var isUserNameExist = await _service.IsUserNameExistAsync(admin.UserName, adminid);
                if (isUserNameExist == null || isUserNameExist == true)
                    return BadRequest("UserName Already Exist");

                admin.AdminID = adminid;
                var result = await _service.UpdateAsync(admin);
                if (result <= 0)
                    return BadRequest("Failed to Update Admin");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Admin
        /// </summary>
        /// <param name="adminid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/admin/{adminid}")]
        public async Task<IActionResult> Delete(Guid adminid)
        {
            try
            {
                //Check Admin Existance 
                var isUserNameExist = await _service.IsExistAsync(adminid);
                if (isUserNameExist == null || isUserNameExist == false)
                    return BadRequest("Admin Not Exist");

                var result = await _service.DeleteAsync(adminid);
                if (result <= 0)
                    return BadRequest("Failed to Delete Admin");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }
    }
}