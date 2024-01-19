using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargerTypeController : ControllerBase
    {
        private readonly IChargerTypeService _chargerTypeService;

        public ChargerTypeController(IChargerTypeService chargerTypeService)
        {
            _chargerTypeService = chargerTypeService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllChargerTypes()
        {
            try
            {
                var chargerTypes = await _chargerTypeService.GetAllChargerTypesAsync();
                return Ok(chargerTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{chargerTypeId}")]
        public async Task<IActionResult> GetChargerTypeById(Guid chargerTypeId)
        {
            try
            {
                var chargerType = await _chargerTypeService.GetChargerTypeByIdAsync(chargerTypeId);

                if (chargerType == null)
                {
                    return NotFound($"Charger Type with ID {chargerTypeId} not found.");
                }

                return Ok(chargerType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddChargerType([FromBody] ChargerTypeDto chargerTypeDto)
        {
            try
            {
                var result = await _chargerTypeService.AddChargerTypeAsync(chargerTypeDto);

                if ( !result.Succeeded)
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

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateChargerType([FromBody] ChargerTypeDto chargerTypeDto)
        {
            try
            {
                var result = await _chargerTypeService.UpdateChargerTypeAsync(chargerTypeDto);

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


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> RemoveChargerType([FromBody] ChargerTypeDto chargerTypeDto)
        {
            try
            {
                var result = await _chargerTypeService.RemoveChargerTypeAsync(chargerTypeDto);

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
