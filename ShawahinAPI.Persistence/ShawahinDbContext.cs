using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Entities.ChargingStationsEntities;
using ShawahinAPI.Core.Entities.CummunityEntities;
using ShawahinAPI.Core.Entities.ServicesEntities;
using ShawahinAPI.Core.Entities.ServicesEntitiess;

namespace ShawahinAPI.Persistence
{
    public class ShawahinDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ShawahinDbContext(DbContextOptions<ShawahinDbContext> options) : base(options)
        {
        }


        public DbSet<ChargingSessions> ChargingSessions { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<ChargerType> ChargerTypes { get; set; }
        public DbSet<Chargers> Chargers { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<ChargingStations> Stations { get; set; }
        public DbSet<ChargingStationRequests> StationRequest { get; set; }
        public DbSet<FavoriteStations> FavoriteStations { get; set; }
        public DbSet<ChargerStationComments> StationComments { get; set; }
        public DbSet<StationOpeningHours> chargingStationsHours { get; set; } 

        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<ServiceInfo> ServiceInfo { get; set; }
        public DbSet<ServiceRequest> ServiceReq { get; set; }
        public DbSet<FavoriteServices> FavoriteServices { get; set; }


        public DbSet<CommunityPosts> CommunityPosts { get; set; }
        public DbSet<CommunityComments> CommunityComments { get; set; }
        public DbSet<CommunityEvents> CommunityEvents { get; set; }
        public DbSet<CommunityEvNews> EvNews { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure IdentityUserLogin
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            //});

            //modelBuilder.Entity<ChargingSessions>()
            //    .HasOne(session => session.Customer)
            //    .WithMany(user => user.ChargingSessions)
            //    .HasForeignKey(session => session.CustomerId.ToString())
            //    .IsRequired();
        }
    }

}
