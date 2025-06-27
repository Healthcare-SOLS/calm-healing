using Calm_Healing.DAL.Models;
using Calm_Healing.Respository;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service;
using Calm_Healing.Service.IService;
using Calm_Healing.Utilities;
using Calm_Healing.Utilities.IUtilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Serilog;
using System.IO;

//var logDirectory = "C:\\Users\\LNV-149\\Desktop\\NewProject_MH\\calm-healing\\Calm&Healing\\Calm&Healing\\Logs";
//\bin\Debug\net8.0\Logs\
var logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
// ?? Create Logs directory if it doesn't exist
Directory.CreateDirectory(logDirectory);

// ?? Define log path with rolling log files
var logPath = Path.Combine(logDirectory, "log-.txt");
// ?? Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose() // ?? Capture all log levels
    .WriteTo.Console()
    .WriteTo.File(
        path: logPath,
        rollingInterval: RollingInterval.Day,              // ?? One log file per day
        retainedFileCountLimit: 7,                         // ?? Keep last 7 days of logs
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.
//JWT TokenAuthentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
}); 

//DB service registration

builder.Services.AddDbContext<TenantDbContext>((provider, options) =>
{
    var currentUserService = provider.GetRequiredService<ICurrentUserService>();
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

    // Call constructor manually with options + service
   // options.UseInternalServiceProvider(provider);
});
builder.Services.AddDbContext<TenantDbContext>((provider, options) =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var currentUserService = provider.GetRequiredService<ICurrentUserService>();
    options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
});

//Service registration
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<IAESHelper, AESHelper>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISendEmail, SendEmail>();
builder.Services.AddScoped<IProvideUserService, ProvideUserService>();
builder.Services.AddScoped<IProvideUserRepository, ProvideUserRepository>();
builder.Services.AddScoped<IProviderGroupRepository, ProviderGroupRepository>();
builder.Services.AddScoped<IProviderGroupService, ProviderGroupService>();
builder.Services.AddScoped<IGenericRepositoryFactory, GenericRepositoryFactory>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
});
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
