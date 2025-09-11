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
        
        var response = await _stockdataservice.parametersStock();
        return Ok(response);
        
        
        
    }

    [HttpGet("/get/all_calc_data")]
    public async Task<IActionResult> GetAllCalcData()
    {
        var response = await _generated_data.stock_calculus_base();
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
    
}