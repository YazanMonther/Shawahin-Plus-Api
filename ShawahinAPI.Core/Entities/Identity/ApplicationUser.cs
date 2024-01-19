using Microsoft.AspNetCore.Identity;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities.CummunityEntities;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace ShawahinAPI.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

    
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public UserRole UserRole { get; set; }
        public string? ProfileImageUrl { get; set; }
        
        
        public ICollection<CommunityPosts>? CommunityPosts { get; set; }
        public ICollection<CommunityEvents>? CommunityEvents { get; set; }
        public ICollection<CommunityEvNews>? CommunityEvNewsList { get; set; }
        public ICollection<ServiceRequest>? ServiceRequests { get; set; }
        public ICollection<FavoriteServices>? FavoriteServices { get; set; }
        public ICollection<ChargingStations>? ChargingStations { get; set; }
        public ICollection<ChargingStationRequests>? ChargingStationsRequests { get; set; }
        public ICollection<Bookings>? bookings { get; set; }

    }
}
