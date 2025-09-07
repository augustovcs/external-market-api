using System.Text.Json.Serialization;
using api_external_scrapper.Services;
using Swashbuckle.AspNetCore;
using api_external_scrapper.Interfaces;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IStockDataService, stockDataService>();
builder.Services.AddScoped<IGeneratedData, generated_data>();



var app = builder.Build();

/*
var service01 = app.Services.GetRequiredService<stockDataService>();
service01.parametersStock();

var service_data = app.Services.GetRequiredService<generated_data>();
service_data.stock_calculus_base();
*/


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
	
}


app.UseHttpsRedirection();


app.Run();


