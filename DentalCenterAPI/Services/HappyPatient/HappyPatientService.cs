using System.Threading.Tasks;
using System.Linq;
using DentalCenterAPI.Models.HappyPatient.Business;
using DentalCenterAPI.Repository.HappyPatient;
using System;
using DentalCenterAPI.Configurations.Logging;
using System.Collections.Generic;
using DentalCenterAPI.Models.HappyPatient.Basic;

namespace DentalCenterAPI.Services.HappyPatient
{
    public class HappyPatientService : IHappyPatientService
    {
        private HappyPatientRepository _repo;
        public HappyPatientService(HappyPatientRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get All Happy Patients With Two Lists (Favorite, All Videos) Ordered by Date, Favorite (For Web)
        /// </summary>
        /// <param name="type">video, review</param>
        /// <param name="isFavorite"></param>
        /// <returns>Return Two Lists of Happy Paitents Favorite, Normal</returns>
        public async Task<HappyPatientBusinessModel> GetAllAsync(string type, bool? isFavorite)
        {
            try
            {
                //All Data
                var paitents = await _repo.GetAllAsync(type.ToLower(), isFavorite);
                HappyPatientBusinessModel result = new HappyPatientBusinessModel();
                //Get Favorite Patients
                result.FavoroitePatients = paitents.Where(x => x.IsFavorite != null && x.IsFavorite == true).OrderByDescending(x => x.InsertDate);
                //Get All Patients
                result.AllPatients = paitents.OrderByDescending(x => x.InsertDate);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get All Happy Patients By Type(video, review) Ordered by InsertDate(By Default), IsFavorite, search (For Admin)
        /// </summary>
        /// <param name="type">video, review</param>
        /// <param name="sortBy">insertdate, isfavorite, name, job, comment</param>
        /// <param name="search">name, job, comment</param>
        /// <param name="orderByAsc">True, False</param>
        /// <returns>Return Two Lists of Happy Paitents</returns>
        public async Task<IEnumerable<HappyPatientBasicModel>> GetAllAsync(string type, string search, string sortBy, bool orderByAsc)
        {
            try
            {
                //All Data
                var result = await _repo.GetAllAsync(type.ToLower(), null);
                if (!string.IsNullOrEmpty(search))
                    result = result.Where(x => x.Name?.ToLower().Contains(search.ToLower()) == true || x.Job?.ToLower().Contains(search.ToLower()) == true
                                        || x.Comment?.ToLower().Contains(search.ToLower()) == true);

                switch (sortBy.ToLower())
                {
                    case "name":
                        result = (orderByAsc) ? result.OrderBy(x => x.Name) : result.OrderByDescending(x => x.Name);
                        break;
                    case "insertdate":
                        result = (orderByAsc) ? result.OrderBy(x => x.InsertDate) : result.OrderByDescending(x => x.InsertDate);
                        break;
                    case "isfavorite":
                        result = (orderByAsc) ? result.OrderByDescending(x => x.IsFavorite).ThenByDescending(x => x.InsertDate) : result.OrderBy(x => x.IsFavorite).ThenBy(x => x.InsertDate);
                        break;

                    default:
                        result = (orderByAsc) ? result.OrderBy(x => x.InsertDate) : result.OrderByDescending(x => x.InsertDate);
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Happy Patient By ID (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <param name="type"></param>
        /// <returns>Return Happy Patient Details</returns>
        public async Task<HappyPatientBasicModel> GetByIDAsync(Guid happyPatientID, string type)
        {
            try
            {
                var result = await _repo.GetByIDAsync(happyPatientID, type.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Random Happy Patient By Type(For Web)
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Return Random Happy Patient Details</returns>
        public async Task<HappyPatientBasicModel> GetRendomBTypeAsync(string type)
        {
            try
            {
                var result = await _repo.GetRandomByTypeAsync(type.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Happy Patient Existance (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid happyPatientID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(happyPatientID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add List of Happy Patients  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        public async Task<int> AddAsync(IEnumerable<HappyPatientBasicModel> model)
        {
            try
            {
                model = model.Select(x => { x.InsertDate = Utility.Utility.GetDateTimeByTimeZone(); return x; });
                var result = await _repo.AddAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Update Happy Patient IsFavorite  (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <param name="isFavorite"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateIsFavoriteAsync(Guid happyPatientID, bool isFavorite)
        {
            try
            {
                var result = await _repo.UpdateIsFavoriteAsync(happyPatientID, isFavorite);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Update Happy Patient  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(HappyPatientBasicModel model)
        {
            try
            {
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
        /// Delete Happy Patient (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid happyPatientID)
        {
            try
            {
                var result = await _repo.DeleteAsync(happyPatientID);
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