using ProyectoWeb.Handlers;
using Serilog;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
#region Logs
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
configuration.WriteTo.Console();
configuration.MinimumLevel.Warning();
configuration.WriteTo.File("Logs/LogWeb.txt", rollingInterval: RollingInterval.Day);
}); 
#endregion


// Add services to the container.
builder.Services.AddControllersWithViews();

//Inyeccion de Dependecia
#region Inyeccion Dependencia
DependencyInyectionHandler.DependencyInyectionConfig(builder.Services);
#endregion

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
