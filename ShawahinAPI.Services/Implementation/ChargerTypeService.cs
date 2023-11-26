using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Services.Implementation
{
    public class ChargerTypeService : IChargerTypeService
    {
        private readonly IChargerTypeRepository _chargerTypeRepository;

        public ChargerTypeService(IChargerTypeRepository chargerTypeRepository)
        {
            _chargerTypeRepository = chargerTypeRepository;
        }

        public Task<ResultDto?> AddChargerTypeAsync(ChargerType? chargerType)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ChargerTypeResponseDto?>> GetAllChargerTypesAsync()
        {
            var chargerTypes = await _chargerTypeRepository.GetAllChargerTypesAsync();

            return chargerTypes.Select(chargerType => new ChargerTypeResponseDto
            {
                Id = chargerType?.Id  ,
                ChargerType = chargerType?.Charger_Type?.ToString(),
                ChargerLogoUrl = chargerType?.ChargerLogoUrl ?? string.Empty
            });
        }

        public Task<ChargerTypeResponseDto?> GetChargerTypeByIdAsync(Guid? chargerTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto?> RemoveChargerTypeAsync(ChargerType chargerType)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto?> UpdateChargerTypeAsync(ChargerType chargerType)
        {
            throw new NotImplementedException();
        }


    }

}

