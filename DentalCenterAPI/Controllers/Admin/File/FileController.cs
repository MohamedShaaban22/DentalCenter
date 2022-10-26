using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.File
{
    [Authorize]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService _service;
        public FileController(IFileService service)
        {
            _service = service;
        }

        /// <summary>
        /// Save file to Server at resourses folder 
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Return saved file path</returns>
        [HttpPost("api/v1/file/save"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return null;

                var result = await _service.UploadFileAsync(file);
                if (result != null)
                    return Ok(result);
                return BadRequest("Failed to save file");
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Save files to Server at resourses folder 
        /// </summary>
        /// <param name="files"></param>
        /// <returns>Return List of aved files paths</returns>
        [HttpPost("api/v1/files/save"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFiles(IEnumerable<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count() <= 0)
                    return null;

                var result = await _service.UploadFilesAsync(files);
                if (result == null || result.Count() <= 0)
                    return BadRequest("Failed to save files");
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