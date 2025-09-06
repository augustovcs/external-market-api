using System.Text.Json.Serialization;
using api_external_scrapper.Services;
var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    
});

builder.Services.AddSingleton<stockDataService>();



var app = builder.Build();

var service01 = app.Services.GetRequiredService<stockDataService>();
service01.parametersStock();


app.Run();


