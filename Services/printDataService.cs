using api_external_scrapper.DTO;
using api_external_scrapper.Interfaces;

namespace api_external_scrapper.Services;

public class printDataService : IPrintDataService

{
    public readonly IGeneratedData _geneneratedData;
    
    
    public printDataService(IGeneratedData geneneratedData)
    {
        _geneneratedData = geneneratedData;
        
    }

    public async Task<List<GeneralCalculusData>> GetVolatility()
    {
        List<GeneralCalculusData> volatilities_list = new List<GeneralCalculusData>();
        volatilities_list = await  _geneneratedData.stock_calculus_base();

        List<GeneralCalculusData> volatility_total = new List<GeneralCalculusData>();
        foreach (var item in volatilities_list)
        {
            volatility_total.Add(item);

        }

        return volatility_total;


    }

    public async Task<List<PercentualReturnData>> GetPercentualReturn()
    {
        
        List<PercentualReturnData> percentualreturn_list = new List<PercentualReturnData>();
        var loader = await _geneneratedData.stock_calculus_base();
        percentualreturn_list = await  _geneneratedData.CalcPercentualReturn();
        
        List<PercentualReturnData> percentual_total = new List<PercentualReturnData>();
        foreach (var item in percentualreturn_list)
        {
            percentual_total.Add(item);
        }
        
        return percentual_total;
        
        
    }
    
    
    
    
}