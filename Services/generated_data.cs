using System.Globalization;
using api_external_scrapper.DTO;
using api_external_scrapper.Services;
using CsvHelper;
using System.Linq;
using System.Text;
using api_external_scrapper.Interfaces;

namespace api_external_scrapper.Services;

public class generated_data : IGeneratedData
{
    public readonly IStockDataService _stockDataService;

    
    //variables scoped class
    private IEnumerable<StockData> open_value;
    private IEnumerable<StockData> close_value;
    private IEnumerable<StockData> low_value;
    private IEnumerable<StockData> high_value;
    private double max_reach;
    private double min_reach;
    private DateTime oldest_date;
    private DateTime newest_date;
    private int date_30days_count;
    private int date_15days_count;
    public int date_7days_count;
    private int date_3days_count;
    private double volatility30Days;
    private double volatility15Days;
    private double volatility7Days;
    private double volatility3Days;
    private double percentualDaily;
    private double percentual7Days;
    private double percentual14Days;
    private double percentual30Days;
    private double percentual60Days;
    private double percentual90Days;
    private string positiveMid30days;
    private string negativeMid30days;
    private string brokenMid30days;
    private IEnumerable<double> close90Days;
    private IEnumerable<double> close60Days;
    private IEnumerable<double> close30Days;
    private IEnumerable<double> close14Days;
    private IEnumerable<double> closeWeekly;
    private IEnumerable<double> closeYesterday;
    private IEnumerable<double> closeDaily;
    private double mid30daysValue;
    private double mid30days;
    private List<StockData> mid30daysList;
    private List<Mid30DaysData> mid30days_list;
    private List<VolatilityData> volatilitiesList;
    private PercentualReturnData percentualreturnData;
    private List<PercentualReturnData> percentualreturnList;
 

    public generated_data(IStockDataService stockDataService, IConfiguration configuration)
    {
        _stockDataService = stockDataService;
    }


    public async Task<List<StockData>> BasicValuesData3Month()
    {
        
        List<StockData> list_data = await _stockDataService.GetStockData();

        open_value = list_data.Where(x => x.Open > 1);
        close_value = list_data.Where(x => x.Close > 1);

        low_value = list_data.Where(x => x.Low > 1);
        high_value = list_data.Where(x => x.High > 1);

        /*if (!close_value.Any())
        {
            throw new Exception("close_value null");;
        }*/
        // max and min values in 3 months
        max_reach = list_data.Max(x => x.Close);
        min_reach = list_data.Min(x => x.Close);
        
        return list_data;
    }

    public async Task<List<StockData>> DateValuesData()
    {
        
        List<StockData> list_data = await _stockDataService.GetStockData();

        // max and min date
        oldest_date = list_data.Min(x => x.Date);
        newest_date = list_data.Max(x => x.Date);

        // date days calculus
        var date_30days = list_data
            .OrderByDescending(x => x.Date)
            .Take(30)
            .ToList();
        date_30days_count = date_30days.Count;

        var date_15days = list_data
            .OrderByDescending(x => x.Date)
            .Take(15)
            .ToList();
        date_15days_count = date_15days.Count;

        var date_7days = list_data
            .OrderByDescending(x => x.Date)
            .Take(7)
            .ToList();
        date_7days_count = date_7days.Count;

        var date_3days = list_data
            .OrderByDescending(x => x.Date)
            .Take(3)
            .ToList();
        date_3days_count = date_3days.Count;

        return list_data;
    }

    public async Task<List<StockData>> CloseValuesData()
    {
        List<StockData> list_data = await _stockDataService.GetStockData();

        
        // closes
        closeDaily = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(1)
            .Select(x => x.Close);

        closeYesterday = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Skip(1)
            .Take(1)
            .Select(x => x.Close);

        closeWeekly = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(7)
            .Select(x => x.Close);

        close14Days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(14)
            .Select(x => x.Close);

        close30Days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(30)
            .Select(x => x.Close);

        close60Days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(60)
            .Select(x => x.Close);

        close90Days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(90)
            .Select(x => x.Close);
        
        
        return list_data;
    }

    public async Task<List<StockData>> Mid30DaysData()
    {

        var loaderClose = await CloseValuesData();
        List<StockData> list_data = await _stockDataService.GetStockData();
        
        // 30 last days mid 

        mid30days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Skip(1)
            .Take(30)
            .Average(x => x.Close);

        mid30daysList = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Skip(1)
            .Take(30)
            .ToList();
        
        mid30daysValue = closeDaily.First() - mid30days;

        return list_data;
    }

 
    
    public async Task<List<StockData>> PercentualData()
    {

        var loaderClose = await CloseValuesData();
        List<StockData> list_data = await _stockDataService.GetStockData();
        
        // percentual return in x period
        // close date_x - close date_y / close date_y * 100
        percentualDaily = (closeDaily.First() - closeYesterday.Last()) / closeYesterday.Last() * 100;
        percentual7Days = (closeDaily.First() - closeWeekly.Last()) / closeWeekly.Last() * 100;
        percentual14Days = (closeDaily.First() - close14Days.Last()) / close14Days.Last() * 100;
        percentual30Days = (closeDaily.First() - close30Days.Last()) / close30Days.Last() * 100;
        percentual60Days = (closeDaily.First() - close60Days.Last()) / close60Days.Last() * 100;
        percentual90Days = (closeDaily.First() - close90Days.Last()) / close90Days.Last() * 100;
        
        
        return list_data;
    }

    public async Task<List<VolatilityData>> VolatilityData()
    {
        var loaderBasic = await BasicValuesData3Month();
        var loaderData = await DateValuesData();
        
        List<StockData> list_data = await _stockDataService.GetStockData();
        
        // volatilities 
        volatility30Days = (max_reach - min_reach) / date_30days_count;
        volatility15Days = (max_reach - min_reach) / date_15days_count;
        volatility7Days = (max_reach - min_reach) / date_7days_count;
        volatility3Days = (max_reach - min_reach) / date_3days_count;
        
        
        // all list item
        volatilitiesList = new List<VolatilityData>();

        VolatilityData volatilityData = new VolatilityData
        {
            Volatility3Days = volatility3Days,
            Volatility7Days = volatility7Days,
            Volatility15Days = volatility15Days,
            Volatility30Days = volatility30Days
        };
        
        volatilitiesList.Add(volatilityData);
        return volatilitiesList;

    }

    public async Task<List<PercentualReturnData>> CalcPercentualReturn()
    {
        var loaderClose = await CloseValuesData();
        
        // percentual return in x period
        // close date_x - close date_y / close date_y * 100
        percentualDaily = (closeDaily.First() - closeYesterday.Last()) / closeYesterday.Last() * 100;
        percentual7Days = (closeDaily.First() - closeWeekly.Last()) / closeWeekly.Last() * 100;
        percentual14Days = (closeDaily.First() - close14Days.Last()) / close14Days.Last() * 100;
        percentual30Days = (closeDaily.First() - close30Days.Last()) / close30Days.Last() * 100;
        percentual60Days = (closeDaily.First() - close60Days.Last()) / close60Days.Last() * 100;
        percentual90Days = (closeDaily.First() - close90Days.Last()) / close90Days.Last() * 100;

        percentualreturnData = new PercentualReturnData()
        {
            
            
            PercentualReturn = percentualDaily,
            PercentualReturn7Days = percentual7Days,
            PercentualReturn14Days = percentual14Days,
            PercentualReturn30Days = percentual30Days,
            PercentualReturn60Days = percentual60Days,
            PercentualReturn90Days = percentual90Days
        };
        
        percentualreturnList = new List<PercentualReturnData>();
        percentualreturnList.Add(percentualreturnData);
        
        return percentualreturnList;

    }

    public async Task<List<Mid30DaysData>> CalcMid30Days()
    {
        var loaderMid30Days = await Mid30DaysData();
        
        Mid30DaysData mid30daysData = new Mid30DaysData()
        {
            Mid30Days = mid30daysValue,
            //CloseDaily = closeDaily.First(),
            //Last30Days = mid30daysList
            
        };

        mid30days_list = new List<Mid30DaysData>();
        mid30days_list.Add(mid30daysData);
        
        return mid30days_list;

    }

    public async Task<List<FullData>> CalcFullData()
    {
        
        FullData fullData = new FullData()
        {
            Full_Volatility = volatilitiesList,
            Full_PercentualReturn = percentualreturnList,
            Mid30Days = mid30days_list

        };
        
        //var loader = await _stockDataService.parametersStock();
        var fullDataList = new List<FullData>();
        fullDataList.Add(fullData);
        
        return fullDataList;
        
        
    }
    
    
}
