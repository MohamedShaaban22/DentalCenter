using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Gallery.Basic;
using DentalCenterAPI.Services.Gallery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.Gallery
{
    [Authorize]
    [ApiController]
    public class AdminGalleryController : ControllerBase
    {
        private IGalleryService _service;
        public AdminGalleryController(IGalleryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Gallery Images
        /// </summary>
        /// <param name="search">By Type With Default value (All)</param>
        /// <param name="sortby">Date Asc, Desc</param>
        /// <returns>Return List of Gallery Images</returns>
        [HttpGet("admin/v1/gallery")]
        public async Task<ActionResult> GetAll(string search = "all", string sortby = "desc")
        {
            try
            {
                var result = await _service.GetAllAsync(search, sortby);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add List of Gallerys with same type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gallerys"></param>
        /// <returns>Return Added Rowa Count</returns>
        [HttpPost("admin/v1/gallerytype/{type}/gallery")]
        public async Task<IActionResult> Add(string type, [FromBody] IEnumerable<GalleryBasicModel> gallerys)
        {
            try
            {
                var result = await _service.AddAsync(type, gallerys);
                if (result < 0)
                    return BadRequest("Failed to Add Gallery Images");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Gallery Image Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="galleryid"></param>
        /// <returns>Return Updated rows count</returns>
        [HttpPut("admin/v1/gallerytype/{type}/gallery/{galleryid}")]
        public async Task<IActionResult> Update(string type, Guid galleryid)
        {
            try
            {
                //Check Gallery Existance 
                var isExist = await _service.IsExistAsync(galleryid);
                if (isExist == null || isExist == false)
                    return BadRequest("Gallery Not Exist");

                var result = await _service.UpdateAsync(galleryid, type);
                if (result <= 0)
                    return BadRequest("Failed to Update Gallery");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Gallery Image IsFavorite
        /// </summary>
        /// <param name="type"></param>
        /// <param name="galleryid"></param>
        /// <param name="isfavorite"></param>
        /// <returns>Return Updated rows count</returns>
        [HttpPut("admin/v1/gallerytype/{type}/gallery/{galleryid}/{isfavorite}")]
        public async Task<IActionResult> UpdateIsFavorite(string type, Guid galleryid, bool isfavorite)
        {
            try
            {
                //Check Gallery Existance 
                var isExist = await _service.IsExistAsync(galleryid);
                if (isExist == null || isExist == false)
                    return BadRequest("Gallery Not Exist");

                var result = await _service.UpdateIsFavoriteAsync(galleryid, type, isfavorite);
                if (result <= 0)
                    return BadRequest("Failed to Update Gallery Favorite");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete List of Gallery Images
        /// </summary>
        /// <param name="galleryIDs"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/gallery")]
        public async Task<IActionResult> Delete(IEnumerable<GalleryBasicModel> galleryIDs)
        {
            try
            {
                var result = await _service.DeleteAsync(galleryIDs);
                if (result <= 0)
                    return BadRequest("Failed to Delete Gallery Images");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Gallery Images By ID
        /// </summary>
        /// <param name="galleryid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/gallery/{galleryid}")]
        public async Task<IActionResult> Delete(Guid galleryid)
        {
            try
            {
                var result = await _service.DeleteAsync(new List<GalleryBasicModel>() { new GalleryBasicModel() { GalleryID = galleryid } });
                if (result <= 0)
                    return BadRequest("Failed to Delete Gallery Image");

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