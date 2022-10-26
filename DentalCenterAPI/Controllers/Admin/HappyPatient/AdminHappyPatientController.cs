using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.HappyPatient.Basic;
using DentalCenterAPI.Services.HappyPatient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.HappyPatient
{
    [Authorize]
    [ApiController]
    public class AdminHappyPatientController : ControllerBase
    {
        private IHappyPatientService _service;
        public AdminHappyPatientController(IHappyPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Happy Patients By Type(video, review) Ordered by InsertDate(By Default), IsFavorite, search (For Admin)
        /// </summary>
        /// <param name="type">video, review</param>
        /// <param name="sortby">insertdate, isfavorite, name, job, comment</param>
        /// <param name="search">name, job, comment</param>
        /// <param name="orderbyasc">True, False</param>
        /// <returns>Return Two Lists of Happy Paitents</returns>
        [HttpGet("admin/v1/happypatientstype/{type}/happypatients")]
        public async Task<ActionResult> GetAll(string type, string sortby = "insertdate", bool orderbyasc = true, string search = "")
        {
            try
            {
                var result = await _service.GetAllAsync(type, search, sortby, orderbyasc);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Happy Patient By ID 
        /// </summary>
        /// <param name="happypatientid"></param>
        /// <param name="type">video, review</param>
        /// <returns>Return Happy Patient Details</returns>
        [HttpGet("admin/v1/happypatientstype/{type}/happypatient/{happypatientid}")]
        public async Task<IActionResult> GetByID(Guid happypatientid, string type)
        {
            try
            {
                var result = await _service.GetByIDAsync(happypatientid, type);
                if (result == null)
                    return BadRequest("Failed to Get Happy Patient");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add List of Happy Patients 
        /// </summary>
        /// <param name="type">video, review</param>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        [HttpPost("admin/v1/happypatientstype/{type}/happypatients")]
        public async Task<IActionResult> Add(string type, [FromBody] IEnumerable<HappyPatientBasicModel> model)
        {
            try
            {
                model = model.Select(x => { x.Type = type.ToLower(); return x; });
                var result = await _service.AddAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Add Happy Patients");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Happy Patient IsFavorite 
        /// </summary>
        /// <param name="happypatientid"></param>
        /// <param name="isfavorite"></param>
        /// <returns>Return Updated Rows Count</returns>
        [HttpPut("admin/v1/happypatient/{happypatientid}/favorite/{isfavorite}")]
        public async Task<IActionResult> UpdateIsFavorite(Guid happypatientid, bool isfavorite)
        {
            try
            {
                //Check Happy Patient Existance 
                var isExist = await _service.IsExistAsync(happypatientid);
                if (isExist == null || isExist == false)
                    return BadRequest("Happy Patient Not Exist");

                var result = await _service.UpdateIsFavoriteAsync(happypatientid, isfavorite);
                if (result <= 0)
                    return BadRequest("Failed to Update Happy Patient");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Happy Patient IsFavorite 
        /// </summary>
        /// <param name="happypatientid"></param>
        /// <param name="type">video, review</param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        [HttpPut("admin/v1/happypatientstype/{type}/happypatient/{happypatientid}")]
        public async Task<IActionResult> Update(Guid happypatientid, string type, [FromBody] HappyPatientBasicModel model)
        {
            try
            {
                //Check Happy Patient Existance 
                var isExist = await _service.IsExistAsync(happypatientid);
                if (isExist == null || isExist == false)
                    return BadRequest("Happy Patient Not Exist");

                model.HappyPatientID = happypatientid;
                model.Type = type.ToLower();
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update Happy Patient");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Happy Patient (For Admin)
        /// </summary>
        /// <param name="happypatientid"></param>
        /// <returns>Return Deleted Rows Count</returns>
        [HttpDelete("admin/v1/happypatient/{happypatientid}")]
        public async Task<IActionResult> Delete(Guid happypatientid)
        {
            try
            {
                //Check Happy Patient Existance 
                var isExist = await _service.IsExistAsync(happypatientid);
                if (isExist == null || isExist == false)
                    return BadRequest("Happy Patient Not Exist");

                var result = await _service.DeleteAsync(happypatientid);
                if (result <= 0)
                    return BadRequest("Failed to Delete Happy Patient");

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