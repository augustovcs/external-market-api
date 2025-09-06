using System.Text.Json.Serialization;
using api_external_scrapper.Services;
var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    
});

builder.Services.AddSingleton<stockDataService>();
builder.Services.AddScoped<generated_data>();



var app = builder.Build();

var service01 = app.Services.GetRequiredService<stockDataService>();
service01.parametersStock();

var service_data = app.Services.GetRequiredService<generated_data>();
service_data.stock_calculus_base();


app.Run();


