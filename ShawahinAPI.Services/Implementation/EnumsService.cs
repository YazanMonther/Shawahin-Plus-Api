using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract;


namespace ShawahinAPI.Services.Implementation
{
    public class EnumsService : IEnumsService
    {
        public async Task<IEnumerable<string?>?> GetAllChargerPowerAsync()
        {
            var enumValues = Enum.GetValues(typeof(ChargerPower)).Cast<ChargerPower>().Select(e => ChargerPowerExtensions.GetDescription(e)).ToArray();

            return  await Task.FromResult(enumValues);
        }

        public async Task<IEnumerable<string>?> GetAllCurrentChargerStatusAsync()
        {
            var enumValues = Enum.GetValues(typeof(CurrentChargerStatus)).Cast<CurrentChargerStatus>().Select(e => e.ToString()).ToArray();
            return await Task.FromResult(enumValues);
        }

        public async Task<IEnumerable<string>?> GetAllPaymentMethodsAsync()
        {
            var enumValues = Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().Select(e => e.ToString()).ToArray();
            return await Task.FromResult(enumValues);
        }

        public async Task<IEnumerable<string>?> GetAllPaymentTypesAsync()
        {
            var enumValues = Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(e => e.ToString()).ToArray();
            return await Task.FromResult(enumValues);
        }
    }
}
