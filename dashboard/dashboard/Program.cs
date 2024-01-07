using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using dashboard.Data;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
string? connectionString = builder.Configuration.GetConnectionString("TempSensorDb");
if (connectionString == null) {
    Console.WriteLine("No connection string found");
    return;
}

builder.Services.AddDbContext<mysqlefcore.TempSensorDbContext>(options => options.UseMySQL(connectionString));
builder.Services.AddScoped<TemperatureSensorDataService>();

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
