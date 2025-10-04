using api_external_scrapper.Interfaces;
using api_external_scrapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_external_scrapper.Controllers;

[ApiController]
[Route("[controller]")] 
public class stockDataController : ControllerBase
{
    private IStockDataService _stockdataservice;
    private IGeneratedData _generated_data;
    private IPrintDataService _printdataservice;
    
    public stockDataController(IStockDataService stockdataservice, IGeneratedData generatedData, IPrintDataService printDataService)
    {
        _stockdataservice = stockdataservice;
        _generated_data = generatedData;
        _printdataservice = printDataService;
        
    }
    

    [HttpGet("/get/crudedata")]
    public async Task<IActionResult> GetCrudeData()
    {
        
        var response = await _printdataservice.GetCrudeData();
        return Ok(response);
        
    }
    
    [HttpGet("/get/all_calc_data")]
    public async Task<IActionResult> GetAllCalculus()
    {
        
        var response = await _printdataservice.GetAllCalculus();
        return Ok(response);
        
    }

    [HttpGet("/get/volatilities/")]
    public async Task<IActionResult> GetVolatilities()
    {
        
        var response = await _printdataservice.GetVolatility();
        return Ok(response);
        
    }

    [HttpGet("/get/percentual_return")]
    public async Task<IActionResult> GetPercentualReturn()
    {
        var response = await _printdataservice.GetPercentualReturn();
        return Ok(response);
    }

    [HttpGet("/get/mid30days")]
    public async Task<IActionResult> GetMid30Days()
    {
        var response = await _printdataservice.GetMid30Days();
        return Ok(response);

    }
    
}