using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Services.Contract.IServiceServices;
using System.Data;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                var services = await _servicesService.GetAllServicesAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetByTypeId/{typeId}")]
        public async Task<IActionResult> GetServicesByTypeId(Guid typeId)
        {
            try
            {
                var services = await _servicesService.GetServicesByTypeIdAsync(typeId);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            try
            {
                var service = await _servicesService.GetServicesByIdAsync(id);

                if (service == null)
                {
                    return NotFound($"Service with ID {id} not found.");
                }

                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost("Add/{requestId}/{userId}")]
        public async Task<IActionResult> AddService(Guid requestId, Guid userId)
        {
            try
            {
                var result = await _servicesService.AddServicesAsync(requestId, userId);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="serviceDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateService([FromBody] ServiceResponseDto serviceDto)
        {
            try
            {
                var result = await _servicesService.UpdateServicesAsync(serviceDto);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Only for admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> RemoveService(Guid id)
        {
            try
            {
                var result = await _servicesService.RemoveServicesAsync(id);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Message);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
