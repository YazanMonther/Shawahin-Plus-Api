using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShawahinAPI.Application;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityCommentsRepositories;
using ShawahinAPI.Core.IRepositories.ICommunityRepositories.ICommunityEventsRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Persistence;
using ShawahinAPI.Persistence.Repository.ChargingStationsRepositories;
using ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityCommentsRepositories;
using ShawahinAPI.Persistence.Repository.CommunityRepositories.CommunityEventsRepositories;
using ShawahinAPI.Persistence.Repository.UserRepositories.UserAuthRepositories;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Contract.IUserServices;
using ShawahinAPI.Services.Implementation;
using ShawahinAPI.Services.Implementation.ChargingStationsReqService;
using ShawahinAPI.Services.Implementation.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ShawahinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddEntityFrameworkStores<ShawahinDbContext>()
    .AddTokenProvider<EmailTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


// Repository
builder.Services.AddScoped<ShawahinDbContext>();

builder.Services.AddScoped<IUserGetRepository, UserGetRepository>();
builder.Services.AddScoped<IChargerStationCommentsRepository, ChargerStationCommentsRepository>();

builder.Services.AddScoped<IChargerTypeRepository, ChargerTypeRepository>();
builder.Services.AddScoped<IChargingStationRepository, ChargingStationRepository>();
builder.Services.AddScoped<IChargingStationRequestRepository, ChargingStationRequestRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IUserFavoriteStationsRepository, UserFavoriteStationsRepository>();

builder.Services.AddScoped<ICommunityCommentsAddRepository, CommunityCommentsAddRepository>();
builder.Services.AddScoped<ICommunityCommentsGetByPostIdRepository, CommunityCommentsGetByPostIdRepository>();
builder.Services.AddScoped<ICommunityCommentsRemoveRepository, CommunityCommentsRemoveRepository>();
builder.Services.AddScoped<ICommunityEventsAddRepository, CommunityEventsAddRepository>();
builder.Services.AddScoped<ICommunityEventsGetAllRepository, CommunityEventsGetAllRepository>();
builder.Services.AddScoped<ICommunityEventsRemoveRepository, CommunityEventsRemoveRepository>();
//builder.Services.AddScoped<IEvNewsAddRepository, EvNewsAddRepository>();
//builder.Services.AddScoped<IEvNewsGetAllRepository, EvNewsGetAllRepository>();
//builder.Services.AddScoped<IEvNewsRemoveRepository, EvNewsRemoveRepository>();
//builder.Services.AddScoped<ICommunityPostsAddRepository, CommunityPostsAddRepository>();
//builder.Services.AddScoped<ICommunityPostsGetAllRepository, CommunityPostsGetAllRepository>();
//builder.Services.AddScoped<ICommunityPostsRemoveRepository, CommunityPostsRemoveRepository>();
//builder.Services.AddScoped<IServiceInfoAddRepository, ServiceInfoAddRepository>();
//builder.Services.AddScoped<IServiceAddRepository, ServiceAddRepository>();
//builder.Services.AddScoped<IServiceRequestAddRepository, ServiceRequestAddRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
builder.Services.AddScoped<IUserSignOutRepository, UserSignOutRepository>();

builder.Services.AddScoped<IStationOpeningHoursRepository, StationOpeningHoursRepository>();

// Register Locations repository
builder.Services.AddScoped<ILocationsRepository, LocationsRepository>();

// Register Chargers Repo
builder.Services.AddScoped<IChargersRepository, ChargersRepository>();


//Services

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserSignOutService, UserSignOutService>();
builder.Services.AddScoped<IChargingStationRequestService, ChargingStationRequestService>();
builder.Services.AddScoped<IChargingStationsService, ChargingStationsService>();
builder.Services.AddScoped<IFavoriteStationsService, FavoriteStationsService>();
builder.Services.AddScoped<IChargerTypeService, ChargerTypeService>();
builder.Services.AddScoped<IEnumsService, EnumsService>();
builder.Services.AddScoped<IChargerStationCommentsService, ChargerStationCommentsService>();
var app = builder.Build();
app.UseHsts();

app.UseStaticFiles();
// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShawahingAPI");
        c.RoutePrefix = ""; 
    });

app.UseHttpsRedirection();


app.UseRouting();
// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

// Role Initialization
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        RoleInitializer.Initialize(roleManager).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing roles.");
    }
}

app.UseCors();
app.MapControllers();

app.Run();
