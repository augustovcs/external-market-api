using EMAnalysisWeb.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EMAnalysisWeb.Controllers;

[ApiController]
//THIS IS A CONTROLLER FOR FUNCTIONAL RETURNS, NOT JUST ABOUT STOCK
public class funcReturnController : ControllerBase
{
    private IStockDataService _stockdataservice;
    
    public funcReturnController(IStockDataService stockdataservice)
    {

        _stockdataservice = stockdataservice;

    }

    [HttpGet("data-symbols")]
    public async Task<IActionResult> GetGeneralDataStocks()
    {
        var response = await _stockdataservice.ReturnSymbolDataList();

        return Ok(response);

    }

    [HttpGet("basic-data-symbols")]
    public async Task<IActionResult> GetBasicDataStocks()
    {
        var response = await _stockdataservice.ReturnBasicSymbolData();
        return Ok(response);

    }
    
    
}