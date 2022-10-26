using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.CommonQuestion.Basic;

namespace DentalCenterAPI.Services.CommonQuestion
{
    public interface ICommonQuestionService
    {
        /// <summary>
        /// Get All CommonQuestions (For Admin, Web)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="search">type, question, answer</param>
        /// <returns>Return List of CommonQuestion</returns>
        Task<IEnumerable<CommonQuestionBasicModel>> GetAllAsync(string type, string search);

        /// <summary>
        /// Get CommonQuestion Details By commonQuestionID (For Admin)
        /// </summary>
        /// <param name="commonQuestionID"></param>
        /// <returns>Return CommonQuestion Details</returns>
        Task<CommonQuestionBasicModel> GetByIDAsync(Guid commonQuestionID);

        /// <summary>
        /// Check CommonQuestion Existance (For Admin)
        /// </summary>
        /// <param name="commonQuestionID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid commonQuestionID);

        /// <summary>
        /// Add CommonQuestion  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added CommonQuestion GUID</returns>
        Task<Guid?> AddAsync(CommonQuestionBasicModel model);

        /// <summary>
        /// Update CommonQuestion  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(CommonQuestionBasicModel model);

        /// <summary>
        /// Delete CommonQuestion  (For Admin)
        /// </summary>
        /// <param name="commonQuestionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid commonQuestionID);
    }
}