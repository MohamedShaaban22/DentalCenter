using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Branch.Basic;
using DentalCenterAPI.Services.Branch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.Branch
{
    [Authorize]
    [ApiController]
    public class AdminBranchController : ControllerBase
    {
        private IBranchService _service;
        public AdminBranchController(IBranchService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Branches for Dropdown
        /// </summary>
        /// <returns>Return List of Branches Dropdown That Contains (ID And Name)</returns>
        [HttpGet("admin/v1/dropdown/branch")]
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
        /// Get List of Branches
        /// </summary>
        /// <returns>Return List of Branches</returns>
        [HttpGet("admin/v1/branch")]
        public async Task<ActionResult> GetAll()
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
        /// Get Branch Details By BranchID
        /// </summary>
        /// <param name="branchid"></param>
        /// <returns>Return Branch Details</returns>
        [HttpGet("admin/v1/branch/{branchid}")]
        public async Task<IActionResult> GetByID(Guid branchid)
        {
            try
            {
                var result = await _service.GetByIDAsync(branchid);
                if (result == null)
                    return BadRequest("Failed to get Branch");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Branch
        /// </summary>
        /// <param name="branch"></param>
        /// <returns>Return Added Branch GUID</returns>
        [HttpPost("admin/v1/branch")]
        public async Task<IActionResult> Add([FromBody] BranchBasicModel branch)
        {
            try
            {
                var result = await _service.AddAsync(branch);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add Branch");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Branch
        /// </summary>
        /// <param name="branchid"></param>
        /// <param name="branch"></param>
        /// <returns>Return Updated rows count</returns>
        [HttpPut("admin/v1/branch/{branchid}")]
        public async Task<IActionResult> Update(Guid branchid, [FromBody] BranchBasicModel branch)
        {
            try
            {
                branch.BranchID = branchid;
                var result = await _service.UpdateAsync(branch);
                if (result <= 0)
                    return BadRequest("Failed to Update Branch");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Branch
        /// </summary>
        /// <param name="branchid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/branch/{branchid}")]
        public async Task<IActionResult> Delete(Guid branchid)
        {
            try
            {
                //Check Branch Existance 
                var isExist = await _service.IsExistAsync(branchid);
                if (isExist == null || isExist == false)
                    return BadRequest("Branch Not Exist");

                var result = await _service.DeleteAsync(branchid);
                if (result <= 0)
                    return BadRequest("Failed to Delete Branch");

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