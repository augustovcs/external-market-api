using EMAnalysisWeb.Classes;
using EMAnalysisWeb.Interfaces;
using EMAnalysisWeb.Services;
using EMAnalysisWeb2._0.Client.Pages;
using EMAnalysisWeb2._0.Components;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver = AppJsonContext.Default;
    });




builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    var request = httpContextAccessor.HttpContext?.Request;

    var baseUri = request != null 
        ? $"{request.Scheme}://{request.Host}" 
        : "http://localhost"; // fallback

    return new HttpClient { BaseAddress = new Uri(baseUri) };
});


builder.Services.AddScoped<IStockDataService, stockDataService>();
builder.Services.AddScoped<IGeneratedData, generated_data>();
builder.Services.AddScoped<IPrintDataService, printDataService>();



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetRequiredService<IStockDataService>();
    service.SaveStockListSymbol();
}


app.UseStaticFiles();
app.UseAntiforgery();



app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EMAnalysisWeb2._0.Client._Imports).Assembly);

app.MapControllers();


app.Run();