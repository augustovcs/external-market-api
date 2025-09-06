using System.Text.Json.Serialization;
using api_external_scrapper.Services;
var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    
});


var app = builder.Build();

app.Run();

