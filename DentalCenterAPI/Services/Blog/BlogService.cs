using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Blog.Basic;
using DentalCenterAPI.Models.Blog.Business;
using DentalCenterAPI.Repository.Blog;
using DentalCenterAPI.Services.BlogDetails;

namespace DentalCenterAPI.Services.Blog
{
    public class BlogService : IBlogService
    {
        private BlogRepository _repo;
        private IBlogDetailsService _blogDetailsService;
        private IMapper _mapper;
        public BlogService(BlogRepository repo, IBlogDetailsService blogDetailsService, IMapper mapper)
        {
            _repo = repo;
            _blogDetailsService = blogDetailsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of Blogs (For Admin, Web)
        /// </summary>
        /// <returns>Return List of Blogs</returns>
        public async Task<IEnumerable<BlogBasicModel>> GetAllAsync()
        {
            try
            {
                var result = await _repo.GetAllAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Blog Details By BlogsID (For Admin, Web)
        /// </summary>
        /// <param name="blogID"></param>
        /// <returns>Return Blog Details</returns>
        public async Task<BlogBusinessModel> GetByIDAsync(Guid blogID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(blogID);
                result.Date = result.InsertDate.Value.ToString("MMM dd, yyyy");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Blog Existance (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid blogID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(blogID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Blog (For Admin)
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>Return Added Blog GUID</returns>
        public async Task<Guid?> AddAsync(BlogBusinessModel blog)
        {
            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var blogModel = _mapper.Map<BlogBasicModel>(blog);
                    blogModel.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    var result = await _repo.AddAsync(blogModel);

                    if (result != Guid.Empty)
                    {
                        var addBlogDetailsResult = await _blogDetailsService.AddAsync(result, blog.BlogDetails);
                        if (addBlogDetailsResult > 0)
                        {
                            transactionScope.Complete();
                            return result;
                        }
                    }
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Blog (For Admin)
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(BlogBusinessModel blog)
        {
            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var blogModel = _mapper.Map<BlogBasicModel>(blog);
                    blogModel.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    var result = await _repo.UpdateAsync(blogModel);

                    if (result > 0)
                    {
                        result += await _blogDetailsService.UpdateAsync(blog.BlogID, blog.BlogDetails);
                        if (result > 1)
                        {
                            transactionScope.Complete();
                            return result;
                        }
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }

        /// <summary>
        /// Delete Blog (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid blogID)
        {
            try
            {
                var result = await _repo.DeleteAsync(blogID);
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