using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.CommonQuestion.Basic;
using DentalCenterAPI.Services.CommonQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.CommonQuestion
{
    [Authorize]
    [ApiController]
    public class AdminCommonQuestionController : ControllerBase
    {
        private ICommonQuestionService _service;
        public AdminCommonQuestionController(ICommonQuestionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Common Questions
        /// </summary>
        /// <param name="type"></param>
        /// <param name="search">type, question, answer</param>
        /// <returns>Return List of Common Questions</returns>
        [HttpGet("admin/v1/commonquestion")]
        public async Task<ActionResult> GetAll(string type = "all", string search = "")
        {
            try
            {
                var result = await _service.GetAllAsync(type, search);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Common Question Details By Common QuestionID
        /// </summary>
        /// <param name="commonquestionid"></param>
        /// <returns>Return Branch Details</returns>
        [HttpGet("admin/v1/commonquestion/{commonquestionid}")]
        public async Task<IActionResult> GetByID(Guid commonquestionid)
        {
            try
            {
                var result = await _service.GetByIDAsync(commonquestionid);
                if (result == null)
                    return BadRequest("Failed to get Common Question");

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
        /// <param name="model"></param>
        /// <returns>Return Added Branch GUID</returns>
        [HttpPost("admin/v1/commonquestion")]
        public async Task<IActionResult> Add([FromBody] CommonQuestionBasicModel model)
        {
            try
            {
                var result = await _service.AddAsync(model);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add Common Question");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Common Question
        /// </summary>
        /// <param name="commonquestionid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated rows count</returns>
        [HttpPut("admin/v1/commonquestion/{commonquestionid}")]
        public async Task<IActionResult> Update(Guid commonquestionid, [FromBody] CommonQuestionBasicModel model)
        {
            try
            {
                model.CommonQuestionID = commonquestionid;
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update Common Questions");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Common Question
        /// </summary>
        /// <param name="commonquestionid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/commonquestion/{commonquestionid}")]
        public async Task<IActionResult> Delete(Guid commonquestionid)
        {
            try
            {
                //Check Common Question Existance 
                var isExist = await _service.IsExistAsync(commonquestionid);
                if (isExist == null || isExist == false)
                    return BadRequest("Common Question Not Exist");

                var result = await _service.DeleteAsync(commonquestionid);
                if (result <= 0)
                    return BadRequest("Failed to Delete Common Question");

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