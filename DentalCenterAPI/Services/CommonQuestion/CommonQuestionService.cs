using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.CommonQuestion.Basic;
using DentalCenterAPI.Repository.CommonQuestion;

namespace DentalCenterAPI.Services.CommonQuestion
{
    public class CommonQuestionService : ICommonQuestionService
    {
        private CommonQuestionRepository _repo;
        private IMapper _mapper;
        public CommonQuestionService(CommonQuestionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All CommonQuestions (For Admin, Web)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="search">type, question, answer</param>
        /// <returns>Return List of CommonQuestion</returns>
        public async Task<IEnumerable<CommonQuestionBasicModel>> GetAllAsync(string type, string search)
        {
            try
            {
                var result = await _repo.GetAllAsync(type);
                if (!string.IsNullOrEmpty(search))
                    result = result.Where(x => x.Answer?.ToLower().Contains(search) == true || x.Question?.ToLower().Contains(search) == true || x.Type?.ToLower().Contains(search) == true);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get CommonQuestion Details By commonQuestionID (For Admin)
        /// </summary>
        /// <param name="commonQuestionID"></param>
        /// <returns>Return CommonQuestion Details</returns>
        public async Task<CommonQuestionBasicModel> GetByIDAsync(Guid commonQuestionID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(commonQuestionID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check CommonQuestion Existance (For Admin)
        /// </summary>
        /// <param name="commonQuestionID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid commonQuestionID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(commonQuestionID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add CommonQuestion  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added CommonQuestion GUID</returns>
        public async Task<Guid?> AddAsync(CommonQuestionBasicModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Type))
                    model.Type = "all";
                model.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.AddAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update CommonQuestion  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(CommonQuestionBasicModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Type))
                    model.Type = "all";
                model.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
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
        /// Delete CommonQuestion  (For Admin)
        /// </summary>
        /// <param name="commonQuestionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid commonQuestionID)
        {
            try
            {
                var result = await _repo.DeleteAsync(commonQuestionID);
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