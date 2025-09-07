namespace api_external_scrapper.Interfaces;

public interface IStockDataService
{
    public Task<string> parametersStock();
    
}

public interface IGeneratedData
{
    public Task<bool> stock_calculus_base(); 
}