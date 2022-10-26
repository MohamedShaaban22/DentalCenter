using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.NewsImages.Basic;
using DentalCenterAPI.Models.NewsImages.Business;
using DentalCenterAPI.Repository.NewsImage;

namespace DentalCenterAPI.Services.NewsImage
{
    public class NewsImageService : INewsImageService
    {
        private NewsImageRepository _repo;
        private IMapper _mapper;
        public NewsImageService(NewsImageRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Update newsImage by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> AddAndDeleteAsync(Guid newsID, IEnumerable<NewsImagesBusinessModel> model)
        {
            try
            {
                var newsimageModel = _mapper.Map<IEnumerable<NewsImagesBasicModel>>(model);
                newsimageModel = newsimageModel.Select(x =>
                {
                    x.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    x.NewsID = newsID;
                    return x;
                });
                var result = await _repo.DeleteByNewsIDAsync(newsID);
                result += await _repo.AddAsync(newsimageModel);
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