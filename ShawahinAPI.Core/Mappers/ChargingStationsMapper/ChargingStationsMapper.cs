using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;

namespace ShawahinAPI.Core.Mappers.ChargingStationsMapper
{
    public static class ChargingStationsMapper
    {
        public static async Task<ChargingStations> MapToChargingStationsAsync(
            ChargingStationRequests request,
            IContactRepository contactRepository,
            IStationOpeningHoursRepository hoursRepository,
            ILocationsRepository locationsRepository,
            IChargersRepository chargersRepository)
        {
            Chargers? charger = await chargersRepository.GetByIdAsync(request.ChargesId);
            Contacts? contacts = await contactRepository.GetContactByIdAsync(request.ContactId);
            Locations? locations = await locationsRepository.GetByIdAsync(request.LocationId);
            StationOpeningHours? stationOpeningHours = await hoursRepository.GetByIdAsync(request.StationOpeningHoursId);
            
            if(charger != null)
            charger.CurrentChargerStatus = Enums.CurrentChargerStatus.Available;

            ChargingStations charging = new ChargingStations
            {
                Id = Guid.NewGuid(),
                Chargers = charger, 
                ChargesId =request.ChargesId,
                Contact = contacts,
                ContactId=request.ContactId,
                Location = locations,
                LocationId=request.LocationId,
                StationOpeningHours = stationOpeningHours,
                StationOpeningHoursId=request.StationOpeningHoursId,   
                UserId = request.UserId
            };
            return charging;
        }
    }
}