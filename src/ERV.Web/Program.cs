using ERV.App.Infrastructure.Utils;
using ERV.Web.Resources;
using ERV.Web.Utils.Services;
using ERV.Web.Utils.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (t, f) => f.Create(typeof(Lang)));

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<HtmlHelperOptions>(o => o.ClientValidationEnabled = true);

builder.Services.AddOptions();
builder.Services.Configure<WebAppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IApiClientService, ApiClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
