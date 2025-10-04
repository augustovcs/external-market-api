using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress ?? "http://localhost:5176")
});

await builder.Build().RunAsync();