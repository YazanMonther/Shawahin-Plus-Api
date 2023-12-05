using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ServiceDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Services.Contract.IServiceServices;


namespace ShawahinAPI.Application.Controllers
{
    [Route("api/servicerequests")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestController(IServiceRequestService serviceRequestService )
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ServiceRequestResponseDto>>> GetAllServiceRequests()
        {
            try
            {
                var serviceRequests = await _serviceRequestService.GetAllServiceRequestsAsync();
                
                return Ok(serviceRequests);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request {ex}.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ServiceRequestResponseDto>> GetServiceRequestById(Guid id)
        {
            try
            {
                var serviceRequest = await _serviceRequestService.GetServiceRequestByIdAsync(id);

                if (serviceRequest == null)
                {
                    return NotFound();
                }

                return Ok(serviceRequest);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request {ex}.");
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ResultDto>> AddServiceRequest([FromBody] ServiceAddRequest requestDto )
        {
            try
            {
                // Validate requestDto here if needed

                var result = await _serviceRequestService.AddServiceRequestAsync(requestDto);

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


        [HttpDelete("Remove/{id}")]
        public async Task<ActionResult<ResultDto>> RemoveServiceRequest(Guid id)
        {
            try
            {
                var result = await _serviceRequestService.RemoveServiceRequestAsync(id);

                if (result.Succeeded)
                {
                    return Ok(result);
                }

                return NotFound(result);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"An unexpected error occurred while processing the request : {ex}.");
            }
        }
    }
}
