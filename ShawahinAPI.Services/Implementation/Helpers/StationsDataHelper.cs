
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShawahinAPI.Services.Implementation.Helpers
{
    public static class StationsDataHelper
    {
        public static async Task<IEnumerable<T>?> AddStationsForeignData<T>(
            IEnumerable<T?> requests,
            IUserGetRepository user,
            IContactRepository contactGetById,
            IStationOpeningHoursRepository hours,
            ILocationsRepository locations,
            IChargersRepository chargers,
            IChargerTypeRepository chargerTypeGet) where T : ChargingStationBase // Assuming ChargingStationBase is a common base class or interface
        {
            var result = new List<T>();

            foreach (var req in requests)
            {
                if (req != null)
                {
                    req.Contact = await contactGetById.GetContactByIdAsync(req.ContactId);
                    req.Location = await locations.GetByIdAsync(req.LocationId);
                    req.StationOpeningHours = await hours.GetByIdAsync(req.StationOpeningHoursId);
                    req.Chargers = await chargers.GetByIdAsync(req.ChargesId);

                    if (req is ChargingStationRequests chargingStationRequests && chargingStationRequests.UserId != null)
                    {
                        chargingStationRequests.User = await user.GetUserByIdAsync(chargingStationRequests.UserId.Value);
                    }

                    if (req.Chargers != null)
                        req.Chargers.ChargerType = await chargerTypeGet.GetChargerTypeByIdAsync(req.Chargers.ChargerTypeId);

                    result.Add(req);
                }
            }

            return result;
        }
    }
}
