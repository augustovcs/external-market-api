using api_external_scrapper.DTO;

namespace api_external_scrapper.Interfaces;

public interface IStockDataService
{
    public Task<string> parametersStock();
    List<StockData> GetStockData();
}

public interface IGeneratedData
{
    public Task<List<GeneralCalculusData>> stock_calculus_base(); 
}

public interface IPrintDataService
{
    public Task<List<GeneralCalculusData>> GetVolatility();
    //public Task<StockData> GetPercentualReturn();
}