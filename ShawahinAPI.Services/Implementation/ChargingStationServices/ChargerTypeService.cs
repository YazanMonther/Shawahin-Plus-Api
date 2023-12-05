using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.Mappers;
using ShawahinAPI.Services.Contract.IChargingStationsServices;

namespace ShawahinAPI.Services.Implementation
{
    public class ChargerTypeService : IChargerTypeService
    {
        private readonly IRepository<ChargerType> _chargerTypeRepository;

        public ChargerTypeService(IRepository<ChargerType> chargerTypeRepository)
        {
            _chargerTypeRepository = chargerTypeRepository;
        }

        public async Task<ResultDto?> AddChargerTypeAsync(ChargerTypeDto? chargerType)
        {
            if (chargerType == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid charger type provided." };
            }

            var entity = EntityDtoMapper< ChargerType , ChargerTypeDto>.MapToEntity(chargerType);

            // Parse the string representation of ChargersType to enum value
            entity.Charger_Type = EnumHelper.ParseEnum<ChargersType>(chargerType.ChargerType);

            return await _chargerTypeRepository.AddAsync(entity);
        }


        public async Task<IEnumerable<ChargerTypeResponseDto?>> GetAllChargerTypesAsync()
        {
            var chargerTypes = await _chargerTypeRepository.GetAllAsync();

            var dtos = chargerTypes.Select(chargerType => new ChargerTypeResponseDto
            {
                Id = chargerType?.Id,
                ChargerType = chargerType?.Charger_Type.ToString(),
                ChargerLogoUrl = chargerType?.ChargerLogoUrl ?? string.Empty,
                
            });

            return dtos;
        }


        public async Task<ChargerTypeResponseDto?> GetChargerTypeByIdAsync(Guid? chargerTypeId)
        {
            if (chargerTypeId == null)
            {
                return null;
            }

            var chargerType = await _chargerTypeRepository.GetByIdAsync(chargerTypeId.Value);

            return chargerType != null
                ? EntityDtoMapper<ChargerType, ChargerTypeResponseDto>.MapToDto(chargerType)
                : null;
        }

        public async Task<ResultDto?> RemoveChargerTypeAsync(ChargerTypeDto chargerType)
        {
            if (chargerType == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid charger type provided." };
            }

            var existingChargerType = await _chargerTypeRepository.GetByConditionAsync(c => c.Charger_Type.ToString() == chargerType.ChargerType);

            if (existingChargerType == null || existingChargerType.FirstOrDefault() == null)
            {
                return new ResultDto { Succeeded = false, Message = "Charger type not found." };
            }

            var ExistingChargerTypeFromTheList = existingChargerType.FirstOrDefault() ?? null;

            if (ExistingChargerTypeFromTheList != null)
            {
                // Update properties of existing charger type with values from the DTO
                ExistingChargerTypeFromTheList.Charger_Type = EnumHelper.ParseEnum<ChargersType>(chargerType.ChargerType);
                ExistingChargerTypeFromTheList.ChargerLogoUrl = chargerType.ChargerLogoUrl;
                return await _chargerTypeRepository.RemoveAsync(ExistingChargerTypeFromTheList);
            }

            return new ResultDto
            {
                Succeeded = false,
                Message = "Invalid to Remove the Charger Type."
            };
        }
        public async Task<ResultDto?> UpdateChargerTypeAsync(ChargerTypeDto chargerType)
        {
            if (chargerType == null)
            {
                return new ResultDto { Succeeded = false, Message = "Invalid charger type provided." };
            }

            var existingChargerType = await _chargerTypeRepository.GetByConditionAsync(c => c.Charger_Type.ToString() == chargerType.ChargerType);

            if (existingChargerType == null)
            {
                return new ResultDto { Succeeded = false, Message = "Charger type not found." };
            }

            var ExistingChargerTypeFromTheList = existingChargerType.FirstOrDefault() ?? null;

            if (ExistingChargerTypeFromTheList != null)
            {
                // Update properties of existing charger type with values from the DTO
                ExistingChargerTypeFromTheList.Charger_Type = EnumHelper.ParseEnum<ChargersType>(chargerType.ChargerType);
                ExistingChargerTypeFromTheList.ChargerLogoUrl = chargerType.ChargerLogoUrl;
                return await _chargerTypeRepository.UpdateAsync(ExistingChargerTypeFromTheList);
            }

            return new ResultDto { Succeeded = false, Message = "Invalid to update The charger Type ." };
        }
    }
}

// Real Discount دورات