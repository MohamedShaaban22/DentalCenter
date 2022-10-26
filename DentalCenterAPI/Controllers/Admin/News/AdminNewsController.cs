using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.News.Business;
using DentalCenterAPI.Services.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.News
{
    [Authorize]
    [ApiController]
    public class AdminNewsController : ControllerBase
    {
        private INewsService _service;
        public AdminNewsController(INewsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All News Ordered by Date desc 
        /// </summary>
        /// <returns>Return List of News Details</returns>
        [HttpGet("admin/v1/news")]
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
        /// Get News Details by ID with list of news images
        /// </summary>
        /// <param name="newsid"></param>
        /// <returns>Return News Details with list of Images</returns>
        [HttpGet("admin/v1/news/{newsid}")]
        public async Task<ActionResult> GetByID(Guid newsid)
        {
            try
            {
                var result = await _service.GetByIDAsync(newsid);
                if (result == null)
                    return BadRequest("Failed to Get News Details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add News with list of News Images 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added NewsID</returns>
        [HttpPost("admin/v1/news")]
        public async Task<ActionResult> Add([FromBody] NewsBusinessModel model)
        {
            try
            {
                model.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _service.AddAsync(model);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add News Details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update news Details with list of newsimages by delete list then add it again 
        /// </summary>
        /// <param name="newsid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        [HttpPut("admin/v1/news/{newsid}")]
        public async Task<ActionResult> Update(Guid newsid, [FromBody] NewsBusinessModel model)
        {
            try
            {
                model.NewsID = newsid;
                var result = await _service.UpdateAsync(model);
                if (result < 1)
                    return BadRequest("Failed to Update News Details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete news with list of news images 
        /// </summary>
        /// <param name="newsid"></param>
        /// <returns>Return Deleted Rows Count</returns>
        [HttpDelete("admin/v1/news/{newsid}")]
        public async Task<ActionResult> Delete(Guid newsid)
        {
            try
            {
                //Check news Existance 
                var isExist = await _service.IsExistAsync(newsid);
                if (isExist == null || isExist == false)
                    return BadRequest("News Not Exist");

                var result = await _service.DeleteAsync(newsid);
                if (result <= 0)
                    return BadRequest("Failed to Delete News Details");
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