using BusTicket.Adapter.ConfigModels;
using BusTicket.Adapter.ExternalServices;
using BusTicket.Business;
using BusTicket.Business.Caching;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<OBiletIntegrationSetting>(builder.Configuration.GetSection("OBiletIntegrationSetting"));

builder.Services.AddScoped<IOBiletIntegration, OBiletIntegration>();
builder.Services.AddScoped<IBusLocationStore, BusLocationStore>();
builder.Services.AddScoped<ISessionStore, SessionStore>();
builder.Services.AddScoped<IBusTicketCache, BusTicketCache>();
builder.Services.AddScoped<IBusJourneysStore, BusJourneysStore>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<CustomAuthMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
