using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.File.Business;
using Microsoft.AspNetCore.Http;

namespace DentalCenterAPI.Services.File
{
    public interface IFileService
    {
        /// <summary>
        /// Save file to Server at resourses folder 
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Return saved file path</returns>
        Task<FileBusinessModel> UploadFileAsync(IFormFile file);

        /// <summary>
        /// Save files to Server at resourses folder 
        /// </summary>
        /// <param name="files"></param>
        /// <returns>Return List of aved files paths</returns>
        Task<IEnumerable<FileBusinessModel>> UploadFilesAsync(IEnumerable<IFormFile> files);
    }
}