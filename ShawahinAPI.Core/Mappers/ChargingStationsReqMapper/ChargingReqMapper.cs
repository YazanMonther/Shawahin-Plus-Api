using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Core.Mappers.ChargingStationsReqMapper
{
    public static class ChargingReqMapper
    {
        public static ChargingStationRequests MapToStationsReq(
            AddChargingStationsReqDto reqDto, ApplicationUser? user)
        {
            Guid ChargerTypeId = Guid.NewGuid();
            Guid ChargerId = Guid.NewGuid();
            Guid LocationId = Guid.NewGuid();
            Guid StationOpeningHoursId = Guid.NewGuid();
            var chargingSReq = new ChargingStationRequests();
            chargingSReq.Id = Guid.NewGuid();
            
            if(chargingSReq.Chargers != null)
            chargingSReq.Chargers.ParkingType = reqDto.ParkingType;
            chargingSReq.StationOpeningHours = new StationOpeningHours
            {
                Id = StationOpeningHoursId,
                SundayStartTime = reqDto.SundayStartTime,
                SundayEndTime = reqDto.SundayEndTime,
                MondayStartTime = reqDto.MondayStartTime,
                MondayEndTime = reqDto.MondayEndTime,
                TuesdayStartTime = reqDto.TuesdayStartTime,
                TuesdayEndTime = reqDto.TuesdayEndTime,
                WednesdayStartTime = reqDto.WednesdayStartTime,
                WednesdayEndTime = reqDto.WednesdayEndTime,
                ThursdayStartTime = reqDto.ThursdayStartTime,
                ThursdayEndTime = reqDto.ThursdayEndTime,
                FridayStartTime = reqDto.FridayStartTime,
                FridayEndTime = reqDto.FridayEndTime,
                SaturdayStartTime = reqDto.SaturdayStartTime,
                SaturdayEndTime = reqDto.SaturdayEndTime,
                chargingStationRequests = chargingSReq
            };
            chargingSReq.ContactId = reqDto.UserId;
            chargingSReq.Chargers = new Chargers
            {
                Id = ChargerId,
                ChargerTypeId = ChargerTypeId,
                ChargerName = reqDto.StationName,
                ChargerType = new ChargerType
                {
                    Id = ChargerTypeId,
                    ChargerLogoUrl = reqDto.ChargerImageUrl,
                    Charger_Type = EnumHelper.ParseEnum<ChargersType>( reqDto.ChargerType) ,
                    
                },
                ElectricType = reqDto.ElectricType,
                PowerKw = EnumHelper.ParseEnum<ChargerPower>(reqDto.PowerKw),
                ChargerCost = reqDto.ChargerCost,
                ChargerStatus=reqDto.ChargerStatus,
                ImageUrl= reqDto.ChargerImageUrl,
                ParkingType=reqDto.ParkingType,
                PaymentMethod = EnumHelper.ParseEnum<PaymentMethod>(reqDto.PaymentMethod),
                PaymentType = EnumHelper.ParseEnum<PaymentType>(reqDto.PaymentType),
                
            };
            chargingSReq.Chargers.ChargerCost = reqDto.ChargerCost ?? 0;
            chargingSReq.ChargesId = ChargerId;
            chargingSReq.Chargers.ImageUrl = reqDto.ChargerImageUrl;
            chargingSReq.LocationId = LocationId;
            chargingSReq.Location = new Locations
            {
                Id = LocationId,
                BuildingNumber = reqDto.BuildingNumber,
                Country = reqDto.Country,
                Latitude = reqDto.Latitude ?? 0,
                Longitude = reqDto.Longitude ?? 0,
                StreetName = reqDto.StreetName,
                Town = reqDto.Town,
                ChargingStationRequests = new List<ChargingStationRequests> // Initialize the list
                {
                    chargingSReq
                } ,
                City = reqDto.City 
            };
            chargingSReq.UserId = reqDto.UserId;
           // chargingSReq.Request_Status = EnumHelper.ParseEnum<RequestStatus>(reqDto.RequestStatus);
            chargingSReq.Contact = new Contacts()
            {
                Id = Guid.NewGuid(),
                Name = reqDto.ContactName,
                Email = reqDto.Email,
                Phone = reqDto.Phone,
                ChargingStationRequests = new List<ChargingStationRequests> // Initialize the list
                {
                    chargingSReq
                },
                
            };
            chargingSReq.User = user;
            

            return chargingSReq;
        }


        public static ChargingStationsReqResponseDto MapToStationsDto(ChargingStationRequests req)
        {
            return  new ChargingStationsReqResponseDto
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
                    UserId = req.UserId ?? Guid.Empty,
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
                    RequestStatus = req.Request_Status.ToString(),
                    StationName = req.Chargers?.ChargerName,
                    PaymentType = req.Chargers?.PaymentType.ToString(),
                    City = req.Location?.City ,
                    
                };

        }

    }
}

