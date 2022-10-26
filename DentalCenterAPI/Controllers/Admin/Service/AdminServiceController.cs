using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Service.Basic;
using DentalCenterAPI.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.Service
{
    [Authorize]
    [ApiController]
    public class AdminServiceController : ControllerBase
    {
        private IServiceService _service;
        public AdminServiceController(IServiceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All services Ordered by name with search
        /// </summary>
        /// <param name="search">name, info</param>
        /// <param name="sortbyasc">name = True, false</param>
        /// <returns>Return List of service Details</returns>
        [HttpGet("admin/v1/service")]
        public async Task<ActionResult> GetAll(string search = "", bool sortbyasc = true)
        {
            try
            {
                var result = await _service.GetAllAsync(search, sortbyasc);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get service By ID
        /// </summary>
        /// <param name="serviceid"></param>
        /// <returns>Return service Details</returns>
        [HttpGet("admin/v1/service/{serviceid}")]
        public async Task<ActionResult> GetByID(Guid serviceid)
        {
            try
            {
                var result = await _service.GetByIDAsync(serviceid);
                if (result == null)
                    return BadRequest("Failed to get service");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get service By name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return service Details</returns>
        [HttpGet("admin/v1/service/servicename")]
        public async Task<ActionResult> GetByname(string name)
        {
            try
            {
                if(string.IsNullOrEmpty(name))
                    return BadRequest("Must Enter Service Name");
                var result = await _service.GetByNameAsync(name);
                if (result == null)
                    return BadRequest("Failed to get service");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Add service
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added service Guid</returns>
        [HttpPost("admin/v1/service")]
        public async Task<ActionResult> Add([FromBody] ServiceBasicModel model)
        {
            try
            {
                var isNameExist = await _service.IsExistByNameAsync(model.Name);
                if (isNameExist == true || isNameExist == null)
                    return BadRequest("Name Already Exist");

                var result = await _service.AddAsync(model);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add service");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Update service
        /// </summary>
        /// <param name="serviceid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated rows Count</returns>
        [HttpPut("admin/v1/service/{serviceid}")]
        public async Task<ActionResult> Update(Guid serviceid, [FromBody] ServiceBasicModel model)
        {
            try
            {
                var isNameExist = await _service.IsExistByNameAsync(serviceid, model.Name);
                if (isNameExist == true || isNameExist == null)
                    return BadRequest("Name Already Exist");

                model.ServiceID = serviceid;
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update service");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete service
        /// </summary>
        /// <param name="serviceid"></param>
        /// <returns>Return Deleted rows Count</returns>
        [HttpDelete("admin/v1/service/{serviceid}")]
        public async Task<ActionResult> Delete(Guid serviceid)
        {
            try
            {
                var isNameExist = await _service.IsExistByIDAsync(serviceid);
                if (isNameExist == false || isNameExist == null)
                    return BadRequest("Service Not Exists");

                var result = await _service.DeleteAsync(serviceid);
                if (result <= 0)
                    return BadRequest("Failed to Delete service");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }
    }
}