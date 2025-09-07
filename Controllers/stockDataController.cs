using api_external_scrapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_external_scrapper.Controllers;

public class stockDataController : ControllerBase
{
    private stockDataService _stockdataservice;
    
    public stockDataController(stockDataService stockdataservice)
    {
        _stockdataservice = stockdataservice;
        
    }
    

    [Route("[controller]")]
    [HttpGet("/get/crudedata")]
    public IActionResult GetCrudeData()
    {
        
        var response = _stockdataservice.parametersStock();
        return Ok(response);
        
        
        
    }
    
    
    
    
}