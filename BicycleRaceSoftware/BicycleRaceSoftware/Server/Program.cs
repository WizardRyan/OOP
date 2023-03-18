using BicycleRaceSoftware.Server;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using BicycleRaceSoftware.Server.Hubs;
using BicycleRaceSoftware.Server.Controllers;
using Microsoft.AspNetCore.SignalR;
using BicycleRaceSoftware.Shared;

CSVProcessor.ReadFiles();
RacerListController.Racers = CSVProcessor.Racers;

var dReceiver = new DataReceiver();
dReceiver.Start();

var cheatingComputer = new CheatingComputer();

//The cheating computer is now an observer of DataReceived
dReceiver.DataReceived += cheatingComputer.OnRacerStatusReceived;

SignalManager signalManager;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    var hubContext = context.RequestServices
                            .GetRequiredService<IHubContext<RacerStatusHub>>();

    signalManager = new SignalManager(hubContext);
    
    //the SignalManager is now observing the CheaterDetected and DataReceived events
    cheatingComputer.CheaterDetected += signalManager.OnCheaterDetected;
    dReceiver.DataReceived += signalManager.OnDataReceived;

    if (next != null)
    {
        await next.Invoke();
    }
});


app.MapRazorPages();
app.MapControllers();
app.MapHub<RacerStatusHub>("/racerStatusHub");
app.MapFallbackToFile("index.html");

ProgramLogger.myLog = app.Services.GetRequiredService<ILoggerFactory>()
.CreateLogger<Program>();

app.Run();

