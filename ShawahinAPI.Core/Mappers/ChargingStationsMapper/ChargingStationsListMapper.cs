using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShawahinAPI.Core.DTO.ChargingStationsDto;
namespace ShawahinAPI.Core.Mappers.ChargingStationsMapper
{
    public static class ChargingStationsListMapper
    {

        public static IEnumerable<ChargingStationDto?> MapToChargingStationsListDto(IEnumerable<ChargingStations?> chargingStations)
        {
            List<ChargingStationDto>? chargingStationDtos = new List<ChargingStationDto>();

            foreach (var station in chargingStations)
            {
                chargingStationDtos.Add(new ChargingStationDto
                {
                    StationId = station?.Id,
                    Latitude = station?.Location?.Latitude,
                    Longitude = station?.Location?.Longitude,
                    BuildingNumber = station?.Location?.BuildingNumber,
                    StreetName = station?.Location?.StreetName,
                    Town = station?.Location?.Town,
                    Country = station?.Location?.Country,
                    PowerKw = ChargerPowerExtensions.GetDescription(station?.Chargers?.PowerKw),
                    ElectricType = station?.Chargers?.ElectricType,
                    ChargerStatus = station?.Chargers?.ChargerStatus,
                    Rate = station?.Rate,
                    ChargerType = station?.Chargers?.ChargerType?.Charger_Type.ToString(),
                    ChargerImageUrl = station?.Chargers?.ChargerType?.ChargerLogoUrl,
                    ContactName = station?.Contact?.Name,
                    Email = station?.Contact?.Email,
                    Phone = station?.Contact?.Phone,
                    ChargerName = station?.Chargers?.ChargerName,
                    SundayStartTime = station?.StationOpeningHours?.SundayStartTime ?? TimeSpan.Zero,
                    SundayEndTime = station?.StationOpeningHours?.SundayEndTime ?? TimeSpan.Zero,
                    MondayStartTime = station?.StationOpeningHours?.MondayStartTime ?? TimeSpan.Zero,
                    MondayEndTime = station?.StationOpeningHours?.MondayEndTime ?? TimeSpan.Zero,
                    TuesdayStartTime = station?.StationOpeningHours?.TuesdayStartTime ?? TimeSpan.Zero,
                    TuesdayEndTime = station?.StationOpeningHours?.TuesdayEndTime ?? TimeSpan.Zero,
                    WednesdayStartTime = station?.StationOpeningHours?.WednesdayStartTime ?? TimeSpan.Zero,
                    WednesdayEndTime = station?.StationOpeningHours?.WednesdayEndTime ?? TimeSpan.Zero,
                    ThursdayStartTime = station?.StationOpeningHours?.ThursdayStartTime ?? TimeSpan.Zero,
                    ThursdayEndTime = station?.StationOpeningHours?.ThursdayEndTime ?? TimeSpan.Zero,
                    FridayStartTime = station?.StationOpeningHours?.FridayStartTime ?? TimeSpan.Zero,
                    FridayEndTime = station?.StationOpeningHours?.FridayEndTime ?? TimeSpan.Zero,
                    SaturdayStartTime = station?.StationOpeningHours?.SaturdayStartTime ?? TimeSpan.Zero,
                    SaturdayEndTime = station?.StationOpeningHours?.SaturdayEndTime ?? TimeSpan.Zero,
                    ChargerCost = station?.Chargers?.ChargerCost,
                    ParkingType = station?.Chargers?.ParkingType,
                    PaymentMethod = station?.Chargers?.PaymentMethod.ToString(),
                    StationName = station?.Chargers?.ChargerName,
                    UserId = station?.UserId ?? Guid.Empty,
                    PaymentType = station?.Chargers?.PaymentType.ToString(),
                    CurrentChargerStatus = station?.Chargers?.CurrentChargerStatus.ToString(),
                    NumOfFavorites = station?.FavoriteCount  ?? 0 , 
                    City = station?.Location?.City
                }); ;
            }

            return chargingStationDtos;
        }
    }
}
