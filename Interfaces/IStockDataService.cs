using api_external_scrapper.DTO;

namespace api_external_scrapper.Interfaces;

public interface IStockDataService
{

    public string GetRawCSV(string symbol);
    public string SaveRawCSV(string symbol, string content);
    public string ParsingRaw(string symbol, string csv);
    public Task<List<StockData>> parametersStock();
    public Task <List<StockData>> GetStockData();
    //List<ClosesStockData> GetClosesStockData();
}

public interface IGeneratedData
{
    public Task<List<VolatilityData>> stock_calculus_base(); 
    public Task<List<PercentualReturnData>> CalcPercentualReturn();
    //public Task<List<GeneralCalculusData>> CalcVolatility();
    public Task<List<Mid30DaysData>> CalcMid30Days();
    public Task<List<FullData>> CalcFullData();


}

public interface IPrintDataService
{

    public Task<List<StockData>> GetCrudeData();
    public Task<List<FullData>> GetAllCalculus();
    public Task<List<VolatilityData>> GetVolatility();
    public Task<List<PercentualReturnData>> GetPercentualReturn();
    public Task<List<Mid30DaysData>> GetMid30Days();
    
    
}