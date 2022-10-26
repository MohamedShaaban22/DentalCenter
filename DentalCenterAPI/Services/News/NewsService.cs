using System.Threading.Tasks;
using System.Linq;
using DentalCenterAPI.Models.News.Business;
using DentalCenterAPI.Repository.News;
using System;
using DentalCenterAPI.Configurations.Logging;
using System.Collections.Generic;
using DentalCenterAPI.Models.News.Basic;
using AutoMapper;
using DentalCenterAPI.Services.NewsImage;

namespace DentalCenterAPI.Services.News
{
    public class NewsService : INewsService
    {
        private NewsRepository _repo;
        private INewsImageService _newsImageService;
        private IMapper _mapper;
        public NewsService(NewsRepository repo, INewsImageService newsImageService, IMapper mapper)
        {
            _repo = repo;
            _newsImageService = newsImageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All News Ordered by Date desc (For Web, admin)
        /// </summary>
        /// <returns>Return List of News Details</returns>
        public async Task<IEnumerable<HomeNewsBusinessModel>> GetAllAsync()
        {
            try
            {
                var result = await _repo.GetAllAsync();
                var newsModel = _mapper.Map<IEnumerable<HomeNewsBusinessModel>>(result);
                return newsModel;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get News Details by ID with list of news images (For Web, Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns>Return News Details with list of Images</returns>
        public async Task<NewsBusinessModel> GetByIDAsync(Guid newsID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(newsID);
                result.Date = result.InsertDate.Value.ToString("MMM-dd, yyy");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check News Existance (For Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid newsID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(newsID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add News with list of News Images (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added NewsID</returns>
        public async Task<Guid?> AddAsync(NewsBusinessModel model)
        {
            try
            {
                //Update News 
                var newsModel = _mapper.Map<NewsBasicModel>(model);
                newsModel.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var newsID = await _repo.AddAsync(model);
                if (newsID == Guid.Empty) return Guid.Empty;

                //Update News Images
                if (model.NewsImages != null && model.NewsImages.Count() > 0)
                    await _newsImageService.AddAndDeleteAsync(newsID, model.NewsImages);
                return newsID;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update news Details with list of newsimages by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(NewsBusinessModel model)
        {
            try
            {
                //Update News 
                var newsModel = _mapper.Map<NewsBasicModel>(model);
                var result = await _repo.UpdateAsync(model);
                if (result <= 0) return result;

                //Update News Images
                if (model.NewsImages != null && model.NewsImages.Count() > 0)
                    result += await _newsImageService.AddAndDeleteAsync(model.NewsID, model.NewsImages);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Delete news with list of news images (For Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid newsID)
        {
            try
            {
                var result = await _repo.DeleteAsync(newsID);
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