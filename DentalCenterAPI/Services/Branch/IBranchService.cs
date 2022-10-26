using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Branch.Basic;
using DentalCenterAPI.Models.Common;

namespace DentalCenterAPI.Services.Branch
{
    public interface IBranchService
    {
        /// <summary>
        /// Get List of Branches for Dropdown (For Admin, Web)
        /// </summary>
        /// <returns>Return List of Branches Dropdown</returns>
        Task<IEnumerable<DataBusinessModel>> GetAllForDropdownAsync();

        /// <summary>
        /// Get List of Branches for Dropdown (For Admin, Web)
        /// </summary>
        /// <returns>Return List of Branches</returns>
        Task<IEnumerable<BranchBasicModel>> GetAllAsync();

        /// <summary>
        /// Get Branch Details By BranchID (For Admin)
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns>Return Branch Details</returns>
        Task<BranchBasicModel> GetByIDAsync(Guid branchID);

        /// <summary>
        /// Check Branch Existance (For Admin)
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid branchID);

        /// <summary>
        /// Add Branch  (For Admin)
        /// </summary>
        /// <param name="branch"></param>
        /// <returns>Return Added Branch GUID</returns>
        Task<Guid?> AddAsync(BranchBasicModel branch);

        /// <summary>
        /// Update Branch  (For Admin)
        /// </summary>
        /// <param name="branch"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(BranchBasicModel branch);

        /// <summary>
        /// Delete Branch  (For Admin)
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid branchID);
    }
}