using EMAnalysisWeb.DTO;

namespace EMAnalysisWeb.Interfaces;

public interface IStockDataService
{
    
    
    public string GetRawCSV(string symbol);
    public string NewSaveRawCSV();
    public string SaveRawCSV(string symbol);
    public List<StockData> ParsingRaw();
    public List<StockData> parametersStock();
    public Task <List<StockData>> GetStockData();
    public Task <List<SymbolData>> ReturnSymbolDataList();

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
    /*
     TO IMPLEMENT
    public Task<List<StockData>> DailyAverageReturn();
    public Task<List<StockData>> WeeklyAverageReturn();
    public Task<List<StockData>> MonthlyAverageReturn();
    public Task<List<StockData>> YearlyAverageReturn();
    public Task<List<StockData>> CumulativeReturnLastYear();
    public Task<List<StockData>> CumulativeReturnLastMonth();
    public Task<List<StockData>> SharpeRatio();
    public Task<List<StockData>> AnnualizedReturn();
    public Task<List<StockData>> MaxDrawdown();
    */
    
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