using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShawahinAPI.Application;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.IRepositories;
using ShawahinAPI.Core.IRepositories.IChargingStationsRepositories;
using ShawahinAPI.Core.IRepositories.IUserRepository;
using ShawahinAPI.Core.IRepositories.IUserRepository.IUserAuthRepositories;
using ShawahinAPI.Persistence;
using ShawahinAPI.Persistence.Repository;
using ShawahinAPI.Persistence.Repository.ChargingStationsRepositories;
using ShawahinAPI.Persistence.Repository.UserRepositories.UserAuthRepositories;
using ShawahinAPI.Services.Contract;
using ShawahinAPI.Services.Contract.IChargingStationsServices;
using ShawahinAPI.Services.Contract.ICommunityServices;
using ShawahinAPI.Services.Contract.IServiceServices;
using ShawahinAPI.Services.Contract.IUserServices;
using ShawahinAPI.Services.Implementation;
using ShawahinAPI.Services.Implementation.CommunityServices;
using ShawahinAPI.Services.Implementation.ServiceServices;
using ShawahinAPI.Services.Implementation.UserServices;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shawahin API", Version = "v1" });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});



builder.Services.AddDbContext<ShawahinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddEntityFrameworkStores<ShawahinDbContext>()
    .AddTokenProvider<EmailTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


// Repository
builder.Services.AddScoped<ShawahinDbContext>();

builder.Services.AddScoped<IUserGetRepository, UserGetRepository>();

builder.Services.AddScoped<IStationGetRepository, StationGetRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
builder.Services.AddScoped<IUserSignOutRepository, UserSignOutRepository>();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
builder.Services.AddScoped<ICommunityCommentsService , CommunityCommentsService>();
builder.Services.AddScoped<ICommunityEventsService, CommunityEventsService>();
builder.Services.AddScoped<ICommunityPostsService, CommunityPostsService>();
builder.Services.AddScoped<IEvNewsService, EvNewsService>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<IServiceInfoService, ServiceInfoService>();
builder.Services.AddScoped<IServicesService, ServicesService>();

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