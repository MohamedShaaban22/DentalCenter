using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.HomeCounter.Basic;
using DentalCenterAPI.Services.HomeCounter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.HomeCounter
{
    [Authorize]
    [ApiController]
    public class HomeCounterController : ControllerBase
    {
        private IHomeCounterService _service;
        public HomeCounterController(IHomeCounterService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Happy Patients By Type(video, review) Ordered by InsertDate(By Default), IsFavorite, search (For Admin)
        /// </summary>
        /// <returns>Return Home Counter Details</returns>
        [HttpGet("admin/v1/homecounter")]
        public async Task<ActionResult> GetFirst()
        {
            try
            {
                var result = await _service.GetFirstAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Home Counter BY ID(For Admin)
        /// </summary>
        /// <param name="homecounterid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        [HttpPut("admin/v1/homecounter/{homecounterid}")]
        public async Task<ActionResult> Update(Guid homecounterid, [FromBody] HomeCounterBasicModel model)
        {
            try
            {
                model.HomeCounterID = homecounterid;
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update Counters");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }
    }
}