using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Blog.Business;
using DentalCenterAPI.Services.Blog;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.Blog
{
    [ApiController]
    public class WebBlogController : ControllerBase
    {
        private IBlogService _service;
        public WebBlogController(IBlogService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Blogs
        /// </summary>
        /// <returns>Return List of Blogs</returns>
        [HttpGet("api/v1/blog")]
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
        /// Get Blog Details By BlogsID
        /// </summary>
        /// <param name="blogid"></param>
        /// <returns>Return Blog Details</returns>
        [HttpGet("api/v1/blog/{blogid}")]
        public async Task<IActionResult> GetByID(Guid blogid)
        {
            try
            {
                var result = await _service.GetByIDAsync(blogid);
                if (result == null)
                    return BadRequest("Failed to get Blog Details");

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