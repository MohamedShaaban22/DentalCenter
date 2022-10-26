using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.HappyPatient.Basic;
using DentalCenterAPI.Models.HappyPatient.Business;

namespace DentalCenterAPI.Services.HappyPatient
{
    public interface IHappyPatientService
    {
        /// <summary>
        /// Get All Happy Patients With Two Lists (Favorite, All Videos) Ordered by Date, Favorite (For Web)
        /// </summary>
        /// <param name="type">video, review</param>
        /// <param name="isFavorite"></param>
        /// <returns>Return Two Lists of Happy Paitents Favorite, Normal</returns>
        Task<HappyPatientBusinessModel> GetAllAsync(string type, bool? isFavorite);

        /// <summary>
        /// Get All Happy Patients By Type(video, review) Ordered by InsertDate(By Default), IsFavorite, search (For Admin)
        /// </summary>
        /// <param name="type">video, review</param>
        /// <param name="sortBy">insertdate, isfavorite, name, job, comment</param>
        /// <param name="search">name, job, comment</param>
        /// <param name="orderByAsc">True, False</param>
        /// <returns>Return Two Lists of Happy Paitents</returns>
        Task<IEnumerable<HappyPatientBasicModel>> GetAllAsync(string type, string search, string sortBy, bool orderByAsc);

        /// <summary>
        /// Get Happy Patient By ID (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <param name="type"></param>
        /// <returns>Return Happy Patient Details</returns>
        Task<HappyPatientBasicModel> GetByIDAsync(Guid happyPatientID, string type);

        /// <summary>
        /// Get Random Happy Patient By Type(For Web)
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Return Random Happy Patient Details</returns>
        Task<HappyPatientBasicModel> GetRendomBTypeAsync( string type);

        /// <summary>
        /// Check Happy Patient Existance (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid happyPatientID);

        /// <summary>
        /// Add List of Happy Patients  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        Task<int> AddAsync(IEnumerable<HappyPatientBasicModel> model);

        /// <summary>
        /// Update Happy Patient IsFavorite  (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <param name="isFavorite"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateIsFavoriteAsync(Guid happyPatientID, bool isFavorite);

        /// <summary>
        /// Update Happy Patient  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(HappyPatientBasicModel model);

        /// <summary>
        /// Delete Happy Patient (For Admin)
        /// </summary>
        /// <param name="happyPatientID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid happyPatientID);
    }
}