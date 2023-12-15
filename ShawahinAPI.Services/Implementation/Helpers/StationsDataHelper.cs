
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.IRepositories;


namespace ShawahinAPI.Services.Implementation.Helpers
{
    public static class StationsDataHelper
    {

        public static async Task<IEnumerable<T>?> AddStationsForeignData<T>(
            IEnumerable<T?> requests,
            IRepository<ApplicationUser> user,
            IRepository<Contacts> contactGetById,
            IRepository<StationOpeningHours> hours,
            IRepository<Locations> locations,
            IRepository<Chargers> chargers,
            IRepository<ChargerType> chargerTypeGet) where T : ChargingStationBase // Assuming ChargingStationBase is a common base class or interface
        {
            var result = new List<T>();

            foreach (var req in requests)
            {
                if (req != null)
                {
                    req.Contact = await contactGetById.GetByIdAsync(req.ContactId);
                    req.Location = await locations.GetByIdAsync(req.LocationId);
                    req.StationOpeningHours = await hours.GetByIdAsync(req.StationOpeningHoursId);
                    req.Chargers = await chargers.GetByIdAsync(req.ChargesId);

                    if (req is ChargingStationRequests chargingStationRequests && chargingStationRequests.UserId != null)
                    {
                        chargingStationRequests.User = await user.GetByIdAsync(chargingStationRequests.UserId.Value);
                    }

                    if (req.Chargers != null)
                        req.Chargers.ChargerType = await chargerTypeGet.GetByIdAsync(req.Chargers.ChargerTypeId);

                    result.Add(req);
                }
            }

            return result;
        }

        public static async Task<T?> AddStationForeignData<T>(
        T? req,
        IRepository<ApplicationUser> user,
        IRepository<Contacts> contactGetById,
        IRepository<StationOpeningHours> hours,
        IRepository<Locations> locations,
        IRepository<Chargers> chargers,
        IRepository<ChargerType> chargerTypeGet) where T : ChargingStationBase 
        {


                if (req != null)
                {
                    req.Contact = await contactGetById.GetByIdAsync(req.ContactId);
                    req.Location = await locations.GetByIdAsync(req.LocationId);
                    req.StationOpeningHours = await hours.GetByIdAsync(req.StationOpeningHoursId);
                    req.Chargers = await chargers.GetByIdAsync(req.ChargesId);

                    if (req is ChargingStationRequests chargingStationRequests && chargingStationRequests.UserId != null)
                    {
                        chargingStationRequests.User = await user.GetByIdAsync(chargingStationRequests.UserId.Value);
                    }

                    if (req.Chargers != null)
                        req.Chargers.ChargerType = await chargerTypeGet.GetByIdAsync(req.Chargers.ChargerTypeId);

                
                 }

            return req;
        }
    }
}
