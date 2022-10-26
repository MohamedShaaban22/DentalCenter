using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Doctor.Basic;
using DentalCenterAPI.Services.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.Doctor
{
    [Authorize]
    [ApiController]
    public class AdminDoctorController : ControllerBase
    {
        private IDoctorService _service;
        public AdminDoctorController(IDoctorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Doctors For(Admin, Web)
        /// </summary>
        /// <param name="orderbyasc"></param>
        /// <param name="sortby">Name, ReferalNumber</param>
        /// <param name="search">Name, ReferalNumber</param>
        /// <returns>Return List of Doctors</returns>
        [HttpGet("admin/v1/doctor")]
        public async Task<ActionResult> GetAll(bool orderbyasc = true, string sortby = "name", string search = "")
        {
            try
            {
                var result = await _service.GetAllAsync(orderbyasc, sortby, search);
                return Ok(result);
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
        [HttpGet("admin/v1/doctor/dropdown")]
        public async Task<ActionResult> GetAllForDropdown()
        {
            try
            {
                var result = await _service.GetAllForDropdownAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Doctor With Patients, Blogs By Doctor ID
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns>Return Doctor Details with Patients, Blogs </returns>
        [HttpGet("admin/v1/doctor/{doctorid}")]
        public async Task<IActionResult> GetByID(Guid doctorid)
        {
            try
            {
                var result = await _service.GetByIDAsync(doctorid);
                if (result == null)
                    return BadRequest("Failed to Get doctor");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Doctor
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Doctor GUID</returns>
        [HttpPost("admin/v1/doctor")]
        public async Task<IActionResult> Add([FromBody] DoctorBasicModel model)
        {
            try
            {
                var result = await _service.AddAsync(model);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add doctor");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="doctorid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated rows count</returns>
        [HttpPut("admin/v1/doctor/{doctorid}")]
        public async Task<IActionResult> Upadte(Guid doctorid, [FromBody] DoctorBasicModel model)
        {
            try
            {
                //Check Happy Patient Existance 
                var isExist = await _service.IsExistByIDAsync(doctorid);
                if (isExist == null || isExist == false)
                    return BadRequest("Doctor Not Exist");

                model.DoctorID = doctorid;
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update doctor");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete With Patients, Blogs
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/doctor/{doctorid}")]
        public async Task<IActionResult> Delete(Guid doctorid)
        {
            try
            {
                var result = await _service.DeleteAsync(doctorid);
                if (result <= 0)
                    return BadRequest("Failed to Delete doctor");

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