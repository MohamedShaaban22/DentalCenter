using System.Threading.Tasks;
using System.Linq;
using DentalCenterAPI.Models.FamousSection.Business;
using DentalCenterAPI.Repository.FamousSection;
using System;
using DentalCenterAPI.Configurations.Logging;
using System.Collections.Generic;
using DentalCenterAPI.Models.FamousSection.Basic;
using AutoMapper;
using DentalCenterAPI.Services.FamousSection;
using DentalCenterAPI.Services.FamousSectionImage;

namespace DentalCenterAPI.Services.FamousSection
{
    public class FamousSectionService : IFamousSectionService
    {
        private FamousSectionRepository _repo;
        private IFamousSectionImageService _famousSectionImageService;
        private IMapper _mapper;
        public FamousSectionService(FamousSectionRepository repo, IFamousSectionImageService famousSectionImageService, IMapper mapper)
        {
            _repo = repo;
            _famousSectionImageService = famousSectionImageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All FamousSection Ordered by Date desc (For Web, admin)
        /// </summary>
        /// <param name="search">name</param>
        /// <returns>Return List of FamousSection Details</returns>
        public async Task<IEnumerable<FamousSectionBasicModel>> GetAllAsync(string search)
        {
            try
            {
                var result = await _repo.GetAllAsync();
                if (!string.IsNullOrEmpty(search))
                    result = result.Where(x => x.Name?.ToLower().Contains(search.ToLower()) == true).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get FamousSection Details by ID with list of FamousSection images (For Web, Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return FamousSection Details with list of Images</returns>
        public async Task<FamousSectionBusinessModel> GetByIDAsync(Guid famousSectionID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(famousSectionID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check FamousSection Existance (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid famousSectionID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(famousSectionID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add FamousSection with list of FamousSection Images (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added FamousSectionID</returns>
        public async Task<Guid?> AddAsync(FamousSectionBusinessModel model)
        {
            try
            {
                //Update FamousSection 
                var famousSectionModel = _mapper.Map<FamousSectionBasicModel>(model);
                famousSectionModel.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var famousSectionID = await _repo.AddAsync(model);
                if (famousSectionID == Guid.Empty) return Guid.Empty;

                //Update FamousSection Images
                if (model.FamousSectionImages != null && model.FamousSectionImages.Count() > 0)
                    await _famousSectionImageService.AddAndDeleteAsync(famousSectionID, model.FamousSectionImages);
                return famousSectionID;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update FamousSection Details with list of FamousSectionImages by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(FamousSectionBusinessModel model)
        {
            try
            {
                //Update FamousSection 
                var famousSectionModel = _mapper.Map<FamousSectionBasicModel>(model);
                famousSectionModel.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.UpdateAsync(model);
                if (result <= 0) return result;

                //Update FamousSection Images
                if (model.FamousSectionImages != null && model.FamousSectionImages.Count() > 0)
                    result += await _famousSectionImageService.AddAndDeleteAsync(model.FamousSectionID, model.FamousSectionImages);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Delete FamousSection with list of FamousSection images (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid famousSectionID)
        {
            try
            {
                var result = await _repo.DeleteAsync(famousSectionID);
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