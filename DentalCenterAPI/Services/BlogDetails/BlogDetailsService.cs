using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.BlogDetails.Basic;
using DentalCenterAPI.Models.BlogDetails.Business;
using DentalCenterAPI.Repository.BlogDetails;

namespace DentalCenterAPI.Services.BlogDetails
{
    public class BlogDetailsService : IBlogDetailsService
    {
        private BlogDetailsRepository _repo;
        private IMapper _mapper;
        public BlogDetailsService(BlogDetailsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Add Blog Details (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        public async Task<int> AddAsync(Guid blogID, IEnumerable<BlogDetailsBusinessModel> model)
        {
            try
            {
                var blogDetailsModel = _mapper.Map<IEnumerable<BlogDetailsBasicModel>>(model);
                blogDetailsModel = blogDetailsModel.Select(x =>
                {
                    x.BlogID = blogID;
                    x.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    return x;
                });
                var result = await _repo.AddAsync(blogDetailsModel);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Update Blog Details (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(Guid blogID, IEnumerable<BlogDetailsBusinessModel> model)
        {
            try
            {
                var blogDetailsModel = _mapper.Map<IEnumerable<BlogDetailsBasicModel>>(model);
                blogDetailsModel = blogDetailsModel.Select(x =>
                {
                    x.BlogID = blogID;
                    x.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    return x;
                });
                var result = await _repo.UpdateAsync(blogDetailsModel);
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