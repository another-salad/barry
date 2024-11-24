using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using dashboard.Data;
using mysqlefcore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
string? connectionString = builder.Configuration.GetConnectionString("TempSensorDb");
if (connectionString == null) {
    Console.WriteLine("No connection string found");
    return;
}

builder.Services.AddDbContext<SensorDBContext>(options => options.UseMySQL(connectionString));
// Register the conrollers
builder.Services.AddScoped<TempSensorController>();
builder.Services.AddScoped<HumiditySensorController>();
// Add controller services
builder.Services.AddScoped<SensorDataService<TempSensorController, tempCData>>();
builder.Services.AddScoped<SensorDataService<HumiditySensorController, humidityData>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
