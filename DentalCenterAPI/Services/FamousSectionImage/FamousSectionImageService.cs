using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.FamousSection.Basic;
using DentalCenterAPI.Models.FamousSection.Business;
using DentalCenterAPI.Models.NewsImages.Basic;
using DentalCenterAPI.Models.NewsImages.Business;
using DentalCenterAPI.Repository.FamousSectionImage;

namespace DentalCenterAPI.Services.FamousSectionImage
{
    public class FamousSectionImageService : IFamousSectionImageService
    {
        private FamousSectionImageRepository _repo;
        private IMapper _mapper;
        public FamousSectionImageService(FamousSectionImageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Update FamousSectionImages by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> AddAndDeleteAsync(Guid famousSectionID, IEnumerable<FamousSectionImageBusinessModel> model)
        {
            try
            {
                var imageModel = _mapper.Map<IEnumerable<FamousSectionImageBasicModel>>(model);
                imageModel = imageModel.Select(x =>
                {
                    x.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    x.FamousSectionID = famousSectionID;
                    return x;
                });
                var result = await DeleteByFamousSectionIDAsync(famousSectionID);
                result += await _repo.AddAsync(imageModel);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }

        /// <summary>
        /// Delete List By famousSectionID (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteByFamousSectionIDAsync(Guid famousSectionID)
        {
            try
            {
                var result = await _repo.DeleteByFamousSectionIDAsync(famousSectionID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }

        /// <summary>
        /// Delete By famousSectionImageID (For Admin)
        /// </summary>
        /// <param name="famousSectionImageID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteByFamousSectionImageIDAsync(Guid famousSectionImageID)
        {
            try
            {
                var result = await _repo.DeleteByFacmousSectionImageIDAsync(famousSectionImageID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }
    }
}