using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IChargingStationsService _chargingStationsService;
        private readonly IChargingStationRequestService _reqChargingStationsService;
        
        public AdminController(IChargingStationsService ChargingStationsService, IChargingStationRequestService ChargingStationsReqService)
        {
            _chargingStationsService = ChargingStationsService;
            _reqChargingStationsService = ChargingStationsReqService;
        }

        [HttpPost("addStation")]
        public async Task<IActionResult> AddChargingStation( Guid? requestId , Guid UserId)
        {
            var result = await _chargingStationsService.AddNewChargingStationAsync(requestId,UserId);
            return Ok(result);
        }

        [HttpGet("getAllReq")]

        public async Task<IActionResult> GetAllChargingStationReq( Guid? UserId)
        {
            if(UserId is null)
            {
                return BadRequest("Invalid request the User Id is Null ");
            }
            var GetAll =await _reqChargingStationsService.GetAllChargingStationRequestsAsync(UserId);
            
            if(GetAll?.Count()>=1)
            return Ok(GetAll);

            return Ok("There is no Requests");
        }


    }
}
