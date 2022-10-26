using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Blog.Business;
using DentalCenterAPI.Services.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.Blog
{
    [Authorize]
    [ApiController]
    public class AdminBlogController : ControllerBase
    {
        private IBlogService _service;
        public AdminBlogController(IBlogService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Blogs
        /// </summary>
        /// <returns>Return List of Blogs</returns>
        [HttpGet("admin/v1/blog")]
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
        [HttpGet("admin/v1/blog/{blogid}")]
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

        /// <summary>
        /// Add Blog with list of blog details
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>Return Added Blog GUID</returns>
        [HttpPost("admin/v1/blog")]
        public async Task<IActionResult> Add([FromBody] BlogBusinessModel blog)
        {
            try
            {
                var result = await _service.AddAsync(blog);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add blog");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Blog 
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="blog"></param>
        /// <returns>Return Updated Rows Count</returns>
        [HttpPut("admin/v1/blog/{blogid}")]
        public async Task<IActionResult> Update(Guid blogid, [FromBody] BlogBusinessModel blog)
        {
            try
            {
                blog.BlogID = blogid;
                var result = await _service.UpdateAsync(blog);
                if (result <= 0)
                    return BadRequest("Failed to Update blog");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Blog By BlogID
        /// </summary>
        /// <param name="blogid"></param>
        /// <returns>Return Deleted Rows Count</returns>
        [HttpDelete("admin/v1/blog/{blogid}")]
        public async Task<IActionResult> Delete(Guid blogid)
        {
            try
            {
                //Check Branch Existance 
                var isExist = await _service.IsExistAsync(blogid);
                if (isExist == null || isExist == false)
                    return BadRequest("Blog Not Exist");

                var result = await _service.DeleteAsync(blogid);
                if (result <= 0)
                    return BadRequest("Failed to Delete Blog");

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