using api_external_scrapper.DTO;
using api_external_scrapper.Interfaces;

namespace api_external_scrapper.Services;

public class printDataService : IPrintDataService

{
    public readonly IGeneratedData _geneneratedData;
    public readonly IStockDataService _stockDataService;
    
    
    public printDataService(IGeneratedData geneneratedData, IStockDataService stockDataService)
    {
        _geneneratedData = geneneratedData;
        _stockDataService = stockDataService;

    }


    public async Task<List<StockData>> GetCrudeData()
    {
        
        List<StockData> StockDataList = new List<StockData>();
        StockDataList = await _stockDataService.GetStockData();


        return StockDataList;
        
    }

    public async Task<List<FullData>> GetAllCalculus()
    {
        
        List<FullData> fullDataList = new List<FullData>();
        var loader =  _stockDataService.GetStockData();
        
        var loader_volatility = _geneneratedData.VolatilityData();
        var loader_mid30days = await _geneneratedData.CalcMid30Days();
        var loader_percentualreturn = await _geneneratedData.CalcPercentualReturn();
        var loader_basicData = await _geneneratedData.BasicValuesData3Month();
        
        fullDataList = await _geneneratedData.CalcFullData();
        
        return fullDataList;
        
    }
    

    public async Task<List<VolatilityData>> GetVolatility()
    {
        List<VolatilityData> volatilities_list = new List<VolatilityData>();
        volatilities_list = await  _geneneratedData.VolatilityData();

        /*
        List<VolatilityData> volatility_total = new List<VolatilityData>();
        foreach (var item in volatilities_list)
        {
            volatility_total.Add(item);

        }*/

        return volatilities_list;


    }

    public async Task<List<PercentualReturnData>> GetPercentualReturn()
    {
        
        List<PercentualReturnData> percentualreturn_list = new List<PercentualReturnData>();
        var loader = await _geneneratedData.VolatilityData();
        percentualreturn_list = await  _geneneratedData.CalcPercentualReturn();
        
        /*List<PercentualReturnData> percentual_total = new List<PercentualReturnData>();
        foreach (var item in percentualreturn_list)
        {
            percentual_total.Add(item);
        }*/
        
        return percentualreturn_list;
        
        
    }

    public async Task<List<Mid30DaysData>> GetMid30Days()
    {
        
        List<Mid30DaysData> mid30_days_list = new List<Mid30DaysData>();
        var loader =  await _geneneratedData.VolatilityData();
        mid30_days_list = await  _geneneratedData.CalcMid30Days();
        
        /*List<Mid30DaysData> mid30_days = new List<Mid30DaysData>();
        foreach (var item in mid30_days_list)
        {
            mid30_days.Add(item);
        }*/

        return mid30_days_list;
        
        
        
    }
    
    
    
    
}