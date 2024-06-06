using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FlightApp.Application.Interfaces;
using FlightApp.Persistence.Services;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Persistence.Repositories;
using FlightApp.Application.Settings;
using MongoDB.Driver;
using FlightApp.Application;
using FlightApp.Persistence.Context;
using FlightApp.Application.Common.Services;
using FlightApp.Persistence.MiddleWares;
using FlightApp.Persistence;
using FlightApp.Application.Interfaces.Factory;
using FlightApp.Persistence.Services.Factory;
using FlightApp.Application.Services;
using FlightApp.Application.Interfaces.Adapter;
using FlightApp.Persistence.Services.Adapter;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Bind settings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.Configure<AviationApiSettings>(builder.Configuration.GetSection("AviationApiSettings"));
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Register MongoDB context
builder.Services.AddSingleton<MongoDbContext>();

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddTransient<IAviationService, AviationService>();

builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceServices(builder.Configuration);

// Configure MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("FlightAppDB"));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

// Register repositories and services
builder.Services.AddHttpClient();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<ICookieService, CookieService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IResponseFactory, ResponseFactory>();
builder.Services.AddSingleton<ITokenServiceFactory, TokenServiceFactory>();
builder.Services.AddHttpClient<IAviationService, AviationService>();
builder.Services.AddTransient<IAviationServiceAdapter, AviationServiceAdapter>();



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

// Configure HttpClient for AuthenticationService with BaseAddress from configuration
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
    client.BaseAddress = new Uri(apiSettings.BaseAddress);
});


var app = builder.Build();

app.UseMiddleware<RedirectIfAuthenticatedMiddleware>();
app.UseMiddleware<CustomJwtCookieMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
