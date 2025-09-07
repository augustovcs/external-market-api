using api_external_scrapper.DTO;

namespace api_external_scrapper.Interfaces;

public interface IStockDataService
{
    public Task<string> parametersStock();
    List<StockData> GetStockData();
}

public interface IGeneratedData
{
    public Task<bool> stock_calculus_base(); 
}