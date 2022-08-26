using ERV.App.DataAccess.Repositories;
using ERV.App.DataAccess.Repositories.Abstracts;
using ERV.App.Infrastructure.Utils;
using ERV.App.Services;
using ERV.App.Services.Abstracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<WebApiAppSettings>(builder.Configuration.GetSection("AppSettings"));

var appSettings = builder.Configuration.GetSection("AppSettings").Get<WebApiAppSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(setupAction =>
    {
        setupAction.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = appSettings.JwtConfig.ValidIssuer,
            ValidAudience = appSettings.JwtConfig.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtConfig.SigningKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers(setupAction =>
{
    setupAction.ReturnHttpNotAcceptable = true;
    setupAction.RespectBrowserAcceptHeader = true;
    setupAction.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
    setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
    setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

}).AddXmlSerializerFormatters();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();