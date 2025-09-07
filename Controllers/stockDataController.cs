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
    
    public stockDataController(IStockDataService stockdataservice, IGeneratedData generatedData)
    {
        _stockdataservice = stockdataservice;
        _generated_data = generatedData;
        
    }
    

    [HttpGet("/get/crudedata")]
    public async Task<IActionResult> GetCrudeData()
    {
        
        var response = await _stockdataservice.parametersStock();
        return Ok(response);
        
        
        
    }
    
    
    
    
}