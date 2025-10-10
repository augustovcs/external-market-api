using EMAnalysisWeb.DTO;
using EMAnalysisWeb.Interfaces;
using EMAnalysisWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMAnalysisWeb.Controllers;

[ApiController]
public class stockDataController : ControllerBase
{
    private IPrintDataService _printdataservice;
    private IStockDataService _stockdataservice;
    private IGeneratedData _generateddataservice;
    
    public stockDataController(IStockDataService stockdataservice, IGeneratedData generatedData, IPrintDataService printDataService)
    {
        
        _printdataservice = printDataService;
        _stockdataservice = stockdataservice;
        _generateddataservice = generatedData;
        
    }
    
    
    [HttpGet("crudedata")]
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

    [HttpGet("/get/AvgDailyReturn1Year")]
    public async Task<IActionResult> GetAvgDailyReturn1Year()
    {
        var response = await _generateddataservice.DailyAverageReturn();
        return Ok(response);
    }
    
    
    [HttpGet("/get/AvgReturn{type}")]
    public async Task<IActionResult> GetAvgReturnGeneral(TypeAvgSwitch type)
    {
        var DailyResponse = await _generateddataservice.DailyAverageReturn();
        var WeeklyResponse = await _generateddataservice.WeeklyAverageReturn();
        var MonthlyResponse = await _generateddataservice.MonthlyAverageReturn();
        var YearlyResponse = await _generateddataservice.YearlyAverageReturn();

        switch (type)
        {
            case TypeAvgSwitch.Daily:
                return Ok(DailyResponse);
            case TypeAvgSwitch.Weekly:
                return Ok(WeeklyResponse);
            case TypeAvgSwitch.Monthly:
                return Ok(MonthlyResponse);
            case TypeAvgSwitch.Yearly:
                return Ok(YearlyResponse);
            default:
                return BadRequest();
        }
    }
    
}