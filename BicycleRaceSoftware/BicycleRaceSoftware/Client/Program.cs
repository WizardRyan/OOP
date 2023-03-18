using BicycleRaceSoftware.Client;
using BicycleRaceSoftware.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IRacerService, RacerService>();
builder.Services.AddSingleton<ScreenManagerService, ScreenManagerService>();

var host = builder.Build();

var racerService = host.Services.GetRequiredService<IRacerService>();
var httpClient = host.Services.GetRequiredService<HttpClient>();
await racerService.StartHub();
await racerService.LoadRacerList(httpClient);

await host.RunAsync();



