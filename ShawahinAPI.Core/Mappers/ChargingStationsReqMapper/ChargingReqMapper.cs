using ShawahinAPI.Core.DTO.ChargingStationsDto;
using ShawahinAPI.Core.DTO.UserDTO;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}

