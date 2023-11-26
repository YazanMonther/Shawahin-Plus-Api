using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.Entities;


namespace ShawahinAPI.Core.Mappers.ChargingStationsReqMapper
{
    public static class ChargingReqListMapper
    {
        public static IEnumerable<ChargingStationsReqResponseDto> MapToStationsDto(IEnumerable<ChargingStationRequests> reqList)
        {
            return reqList.Select(req =>
            {
                var dto = new ChargingStationsReqResponseDto
                {
                    RequestId = req.Id,
                    ParkingType = req.Chargers?.ParkingType,
                    SundayStartTime = req.StationOpeningHours?.SundayStartTime ?? TimeSpan.Zero,
                    SundayEndTime = req.StationOpeningHours?.SundayEndTime ?? TimeSpan.Zero,
                    MondayStartTime = req.StationOpeningHours?.MondayStartTime ?? TimeSpan.Zero,
                    MondayEndTime = req.StationOpeningHours?.MondayEndTime ?? TimeSpan.Zero,
                    TuesdayStartTime = req.StationOpeningHours?.TuesdayStartTime ?? TimeSpan.Zero,
                    TuesdayEndTime = req.StationOpeningHours?.TuesdayEndTime ?? TimeSpan.Zero,
                    WednesdayStartTime = req.StationOpeningHours?.WednesdayStartTime ?? TimeSpan.Zero,
                    WednesdayEndTime = req.StationOpeningHours?.WednesdayEndTime ?? TimeSpan.Zero,
                    ThursdayStartTime = req.StationOpeningHours?.ThursdayStartTime ?? TimeSpan.Zero,
                    ThursdayEndTime = req.StationOpeningHours?.ThursdayEndTime ?? TimeSpan.Zero,
                    FridayStartTime = req.StationOpeningHours?.FridayStartTime ?? TimeSpan.Zero,
                    FridayEndTime = req.StationOpeningHours?.FridayEndTime ?? TimeSpan.Zero,
                    SaturdayStartTime = req.StationOpeningHours?.SaturdayStartTime ?? TimeSpan.Zero,
                    SaturdayEndTime = req.StationOpeningHours?.SaturdayEndTime ?? TimeSpan.Zero,
                    ChargerCost = req.Chargers?.ChargerCost,
                    UserId =req.UserId ?? Guid.Empty,
                    ChargerType = req.Chargers?.ChargerType?.Charger_Type.ToString(),
                    ChargerImageUrl = req.Chargers?.ImageUrl,
                    PowerKw = ChargerPowerExtensions.GetDescription(req.Chargers?.PowerKw),
                    ElectricType = req.Chargers?.ElectricType,
                    BuildingNumber = req.Location?.BuildingNumber,
                    StreetName = req.Location?.StreetName,
                    Town = req.Location?.Town,
                    Country = req.Location?.Country,
                    Latitude = req.Location?.Latitude,
                    Longitude = req.Location?.Longitude,
                    ContactName = req.Contact?.Name,
                    Email = req.Contact?.Email,
                    Phone = req.Contact?.Phone,
                    ChargerStatus = req.Chargers?.ChargerStatus,
                    PaymentMethod = req.Chargers?.PaymentMethod.ToString(),
                    RequestStatus=req.Request_Status.ToString(),
                    StationName=req.Chargers?.ChargerName,
                    PaymentType = req.Chargers?.PaymentType.ToString() ,
                    City = req.Location?.City
                };

                return dto;
            });
        }
    }
}
