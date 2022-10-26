using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Gallery.Basic;
using DentalCenterAPI.Repository.Gallery;

namespace DentalCenterAPI.Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private GalleryRepository _repo;
        private IMapper _mapper;
        public GalleryService(GalleryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of Gallery with search by Type, Sort By Date Asc, Desc(For Admin, Web)
        /// </summary>
        /// <param name="search">By Type</param>
        /// <param name="sortBy">ASC, Desc By Date</param>
        /// <returns>Return List of Gallery</returns>
        public async Task<IEnumerable<GalleryBasicModel>> GetAllAsync(string search, string sortBy)
        {
            try
            {
                var result = await _repo.GetAllAsync(search);
                switch (sortBy.ToLower())
                {
                    case "asc":
                        result = result.OrderBy(x => x.InsertDate);
                        break;

                    case "desc":
                        result = result.OrderByDescending(x => x.InsertDate);
                        break;

                    default:
                        result = result.OrderBy(x => x.InsertDate);
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
        /// Check Gallery Existance (For Admin)
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid galleryID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(galleryID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add List of Gallery Images by type (For Admin)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gallerys"></param>
        /// <returns>Return Added Rows Count</returns>
        public async Task<int> AddAsync(string type, IEnumerable<GalleryBasicModel> gallerys)
        {
            try
            {
                gallerys = gallerys.Select(x =>
                {
                    x.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                    x.Type = type.ToLower();
                    return x;
                });
                var result = await _repo.AddAsync(gallerys);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Update Gallery Image Type (For Admin)
        /// </summary>
        /// <param name="galleryID"></param>
        /// <param name="type"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(Guid galleryID, string type)
        {
            try
            {
                var result = await _repo.UpdateTypeAsync(galleryID, type.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Update Gallery Image isFavorite (For Admin)
        /// </summary>
        /// <param name="galleryID"></param>
        /// <param name="type"></param>
        /// <param name="isFavorite"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateIsFavoriteAsync(Guid galleryID, string type, bool isFavorite)
        {
            try
            {
                var result = await _repo.UpdateIsFavoriteAsync(galleryID, type.ToLower(), isFavorite);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }

        /// <summary>
        /// Delete List of Gallery  (For Admin)
        /// </summary>
        /// <param name="gallerys"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(IEnumerable<GalleryBasicModel> gallerys)
        {
            {
                try
                {
                    var gallerIDs = gallerys.Select(x => x.GalleryID).ToList();
                    var result = await _repo.DeleteAsync(gallerIDs);
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
}