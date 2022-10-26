using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.CommonQuestion;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.CommonQuestion
{
    [ApiController]
    public class WebCommonQuestionController : ControllerBase
    {
        private ICommonQuestionService _service;
        public WebCommonQuestionController(ICommonQuestionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Common Questions
        /// </summary>
        /// <param name="type"></param>
        /// <param name="search">type, question, answer</param>
        /// <returns>Return List of Common Questions</returns>
        [HttpGet("api/v1/commonquestion")]
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
    }
}