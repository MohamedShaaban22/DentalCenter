using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.File.Business;
using Microsoft.AspNetCore.Http;

namespace DentalCenterAPI.Services.File
{
    public class FileService : IFileService
    {
        /// <summary>
        /// Save file to Server at resourses folder 
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Return saved file path</returns>
        public async Task<FileBusinessModel> UploadFileAsync(IFormFile file)
        {
            try
            {
                //Get Directory to save
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Resourses");
                var realPath = Path.Combine(Utility.Utility.GetSiteURL(), "Resourses");
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                var fileName = Guid.NewGuid().ToString() + file.FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var fullRealPath = Path.Combine(realPath, fileName);

                //Save File
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return new FileBusinessModel() { Path = fullRealPath };
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
        public async Task<IEnumerable<FileBusinessModel>> UploadFilesAsync(IEnumerable<IFormFile> files)
        {
            try
            {
                var filesPaths = new List<FileBusinessModel>();
                foreach (var file in files)
                {
                    var filePath = await UploadFileAsync(file);
                    if (filePath != null)
                        filesPaths.Add(filePath);
                }
                return filesPaths;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }
    }
}