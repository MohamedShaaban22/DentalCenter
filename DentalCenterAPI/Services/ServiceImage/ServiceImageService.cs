using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.ServiceImage.Basic;
using DentalCenterAPI.Repository.ServiceImage;

namespace DentalCenterAPI.Services.ServiceImage
{
    public class ServiceImageService : IServiceImageService
    {
        private ServiceImageRepository _repo;
        private IMapper _mapper;
        public ServiceImageService(ServiceImageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of ServiceImages with search by ServiceName, Sort By Date Asc, Desc(For Admin, Web)
        /// </summary>
        /// <param name="search">By serviceName</param>
        /// <param name="sortBy">ASC, Desc By Date</param>
        /// <returns>Return List of ServiceImages</returns>
        public async Task<IEnumerable<ServiceImageBasicModel>> GetAllAsync(string search, string sortBy)
        {
            try
            {
                var result = await _repo.GetAllAsync(search.ToLower());
                switch (sortBy.ToLower())
                {
                    case "asc":
                        result = result.OrderBy(x => x.InsertDate);
                        break;

                    case "desc":
                        result = result.OrderByDescending(x => x.InsertDate);
                        break;

                    default:
                        result = result.OrderBy(x => x.InsertDate);
                        break;

                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check ServiceImage Existance By ServiceImagesID (For Admin)
        /// </summary>
        /// <param name="serviceImageID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid serviceImageID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(serviceImageID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add List of ServiceImages by ServiceName (For Admin)
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceImages"></param>
        /// <returns>Return Added Rows Count</returns>
        public async Task<int> AddAsync(string serviceName, IEnumerable<ServiceImageBasicModel> serviceImages)
        {
            try
            {
                serviceImages = serviceImages.Select(x =>
                {
                    x.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    x.ServiceName = serviceName.ToLower();
                    return x;
                });
                var result = await _repo.AddAsync(serviceImages);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Update ServiceImages ServiceName,Before, After Images Path (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(ServiceImageBasicModel model)
        {
            try
            {
                model.ServiceName = model.ServiceName.ToLower();
                var result = await _repo.UpdateAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Delete List of ServiceImages By ServiceImagesIDs (For Admin)
        /// </summary>
        /// <param name="ServiceImages"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(IEnumerable<ServiceImageBasicModel> ServiceImages)
        {
            {
                try
                {
                    var sericeImageIDs = ServiceImages.Select(x => x.ServiceImagesID).ToList();
                    var result = await _repo.DeleteAsync(sericeImageIDs);
                    return result;
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex);
                    return -1;
                }
            }
        }
    }
}