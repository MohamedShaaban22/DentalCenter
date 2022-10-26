using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Branch.Basic;
using DentalCenterAPI.Models.Common;
using DentalCenterAPI.Repository.Branch;

namespace DentalCenterAPI.Services.Branch
{
    public class BranchService : IBranchService
    {
        private BranchRepository _repo;
        private IMapper _mapper;
        public BranchService(BranchRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of Branches for Dropdown (ID, Name) (For Admin, Web)
        /// </summary>
        /// <returns>Return List of Branches Dropdown(ID, Name)</returns>
        public async Task<IEnumerable<DataBusinessModel>> GetAllForDropdownAsync()
        {
            try
            {
                var branches = await _repo.GetAllAsync();
                var result = _mapper.Map<IEnumerable<DataBusinessModel>>(branches);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get List of Branches for Dropdown (For Admin, Web)
        /// </summary>
        /// <returns>Return List of Branches</returns>
        public async Task<IEnumerable<BranchBasicModel>> GetAllAsync()
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
        /// Get Branch Details By BranchID (For Admin)
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns>Return Branch Details</returns>
        public async Task<BranchBasicModel> GetByIDAsync(Guid branchID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(branchID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Branch Existance (For Admin)
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid branchID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(branchID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Branch  (For Admin)
        /// </summary>
        /// <param name="branch"></param>
        /// <returns>Return Added Branch GUID</returns>
        public async Task<Guid?> AddAsync(BranchBasicModel branch)
        {
            try
            {
                branch.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.AddAsync(branch);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Branch  (For Admin)
        /// </summary>
        /// <param name="branch"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(BranchBasicModel branch)
        {
            try
            {
                branch.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.UpdateAsync(branch);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Delete Branch  (For Admin)
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid branchID)
        {
            try
            {
                var result = await _repo.DeleteAsync(branchID);
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