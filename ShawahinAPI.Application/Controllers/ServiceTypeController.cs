using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Services.Contract.IServiceServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/servicetypes")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponseDto>>> GetAllServiceTypes()
        {
            try
            {
                var serviceTypes = await _serviceTypeService.GetAllServiceTypesAsync();
                return Ok(serviceTypes);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request {ex}.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServiceTypeResponseDto>> GetServiceTypeById(Guid id)
        {
            try
            {
                var serviceType = await _serviceTypeService.GetServiceTypeByIdAsync(id);

                if (serviceType == null)
                {
                    return NotFound();
                }

                return Ok(serviceType);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request {ex}.");
            }
        }

        /// <summary>
        /// Onlly for admin
        /// </summary>
        /// <param name="serviceTypeDto"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ResultDto>> AddServiceType([FromBody] ServiceTypeBaseDto serviceTypeDto)
        {
            try
            {
                // Validate serviceTypeDto here if needed

                var result = await _serviceTypeService.AddServiceTypeAsync(serviceTypeDto);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request {ex}.");
            }
        }

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serviceTypeDto"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResultDto>> UpdateServiceType(Guid id, [FromBody] ServiceTypeBaseDto serviceTypeDto)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid ID in the request body.");
                }

                // Validate serviceTypeDto here if needed

                var result = await _serviceTypeService.UpdateServiceTypeAsync(serviceTypeDto , id);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request {ex}.");
            }
        }

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Remove/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResultDto>> RemoveServiceType(Guid id)
        {
            try
            {
                var result = await _serviceTypeService.RemoveServiceTypeAsync(id);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request {ex}.");
            }
        }
    }
}
