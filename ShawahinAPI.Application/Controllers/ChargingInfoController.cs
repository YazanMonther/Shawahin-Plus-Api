using Microsoft.AspNetCore.Mvc;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Contract;

namespace ShawahinAPI.Application.Controllers
{
    [ApiController]
    [Route("api/charginginfo")]
    public class ChargingInfoController : ControllerBase
    {
        private readonly IChargerTypeService _chargerType;
        private readonly IEnumsService _paymentMethodService;

        public ChargingInfoController(IChargerTypeService chargerType, IEnumsService paymentMethods)
        {
            _chargerType = chargerType;
            _paymentMethodService = paymentMethods;
        }

        [HttpGet("getChargerTypes")]
        public async Task<IActionResult> GetAllChargerTypes()
        {
            try
            {
                var chargerTypes = await _chargerType.GetAllChargerTypesAsync();
                return Ok(chargerTypes);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve charger types. Please try again later : {ex}");
            }
        }

        [HttpGet("getPaymentMethods")]
        public async Task<IActionResult> GetAllPaymentMethods()
        {
            try
            {
                var paymentMethods = await _paymentMethodService.GetAllPaymentMethodsAsync();
                return Ok(paymentMethods);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve payment methods. Please try again later. {ex}");
            }
        }

        [HttpGet("getPaymentTypess")]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            try
            {
                var paymentTypes = await _paymentMethodService.GetAllPaymentTypesAsync();
                return Ok(paymentTypes);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve payment Types. Please try again later. {ex}");
            }
        }

        [HttpGet("getCurrentChargerStatus")]
        public async Task<IActionResult> GetCurrentChargerStatus()
        {
            try
            {
                var CurrentChargerStatus = await _paymentMethodService.GetAllCurrentChargerStatusAsync();
                return Ok(CurrentChargerStatus);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve Current Charger Status. Please try again later. {ex}");
            }
        }

        [HttpGet("getChargerPower")]
        public async Task<IActionResult> GetChargerPower()
        {
            try
            {
                var ChargerPower = await _paymentMethodService.GetAllChargerPowerAsync();
                return Ok(ChargerPower);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return BadRequest($"Failed to retrieve Charger Power. Please try again later. {ex}");
            }
        }
    }
}
