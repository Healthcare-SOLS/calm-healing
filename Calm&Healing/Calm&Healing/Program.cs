using Calm_Healing.DAL.Models;
using Calm_Healing.Respository;
using Calm_Healing.Respository.IRepository;
using Calm_Healing.Service;
using Calm_Healing.Service.IService;
using Calm_Healing.Utilities;
using Calm_Healing.Utilities.IUtilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB service registration

builder.Services.AddDbContext<TenantDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
