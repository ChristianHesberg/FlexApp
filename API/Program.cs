using Application.DTOs;
using AutoMapper;
using Domain;
using Domain.Models;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Security.Authentication;
using Security.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Byte[] secretBytes = new byte[40];
// Create a byte array with random values. This byte array is used
// to generate a key for signing JWT tokens.
using (var rngCsp = new System.Security.Cryptography.RNGCryptoServiceProvider())
{
    rngCsp.GetBytes(secretBytes);
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    });



builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var mapper = new MapperConfiguration(config =>
{
    config.CreateMap<AddSessionDTO, Session>();
    config.CreateMap<EditSessionDTO, Session>();
    config.CreateMap<AddScheduleDTO, Schedule>();
    config.CreateMap<EditScheduleDTO, Schedule>();
    config.CreateMap<ClockInDTO, Session>();
    config.CreateMap<ClockOutDTO, Session>();
    
}).CreateMapper();

builder.Services.AddSingleton(mapper);

Application.DependencyResolver
    .DependencyResolverService
    .RegisterApplicationLayer(builder.Services);

Infrastructure.DependencyResolver
    .DependencyResolverService
    .RegisterInfrastructure(builder.Services);

Security.DependencyResolver
    .DependencyResolverService
    .RegisterSecurityLayer(builder.Services);

builder.Services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));
builder.Services.AddScoped<UserRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("server=10.176.111.31; database=FlexApp; user=CSe21B_6; password=Water=melon1; encrypt = false;"));
builder.Services.AddDbContext<SecurityContext>(options =>
    options.UseSqlServer("server=10.176.111.31; database=FlexAppSecurity; user=CSe21B_6; password=Water=melon1; encrypt = false;"));

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(opts =>
{
    opts.AllowAnyHeader();
    opts.AllowAnyMethod();
    opts.AllowAnyOrigin();
});

app.Run();