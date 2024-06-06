
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FlightApp.Application.Interfaces;
using FlightApp.Persistence.Services;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Persistence.Repositories;
using FlightApp.Persistence.Context;
using MongoDB.Driver;
using FlightApp.Application.Settings;
using FlightApp.Application;
using FlightApp.Persistence.MiddleWares;
using FlightApp.Application.Common.Services;
using FlightApp.Persistence;
using FlightApp.Application.Interfaces.Factory;
using FlightApp.Persistence.Services.Factory;
using FlightApp.Application.Services;
using FlightApp.Application.Interfaces.Adapter;
using FlightApp.Persistence.Services.Adapter;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Bind MongoDB settings
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<AviationApiSettings>(builder.Configuration.GetSection("AviationApiSettings"));

// Register MongoDB context
builder.Services.AddSingleton<MongoDbContext>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceServices(builder.Configuration);

// Configure MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("FlightAppDB"));
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Register repositories and services
builder.Services.AddHttpClient();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddSingleton<IResponseFactory, ResponseFactory>();
builder.Services.AddSingleton<ITokenServiceFactory, TokenServiceFactory>();
builder.Services.AddHttpClient<IAviationService, AviationService>();
builder.Services.AddTransient<IAviationServiceAdapter, AviationServiceAdapter>();



// Configure Token Service
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ICookieService, CookieService>();


// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


var app = builder.Build();

app.UseMiddleware<RedirectIfAuthenticatedMiddleware>();
app.UseMiddleware<CustomJwtCookieMiddleware>();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MongoDbContext>();
    context.SeedData();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
