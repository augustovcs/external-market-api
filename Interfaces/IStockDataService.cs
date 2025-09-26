using api_external_scrapper.DTO;

namespace api_external_scrapper.Interfaces;

public interface IStockDataService
{

    public string GetRawCSV(string symbol);
    public string SaveRawCSV(string symbol);
    public List<StockData> ParsingRaw();
    public List<StockData> parametersStock();
    public Task <List<StockData>> GetStockData();
    //List<ClosesStockData> GetClosesStockData();
    public string SaveStockListSymbol();

}

public interface IGeneratedData
{
    public Task<List<VolatilityData>> VolatilityData();
    public Task<List<MinMaxData>> MinMaxDataValues();
    public Task<List<StockData>> BasicValuesData();
    public Task<List<StockData>> DateValuesData();
    public Task<List<StockData>> CloseValuesData();
    public Task<List<StockData>> Mid30DaysData();
    public Task<List<StockData>> PercentualData();
    
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