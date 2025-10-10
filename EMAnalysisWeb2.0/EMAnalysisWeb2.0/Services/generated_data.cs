using System.Collections;
using System.Globalization;
using EMAnalysisWeb.DTO;
using EMAnalysisWeb.Services;
using CsvHelper;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using EMAnalysisWeb.Interfaces;

namespace EMAnalysisWeb.Services;

public class generated_data : IGeneratedData
{
    public readonly IStockDataService _stockDataService;

    
    //variables scoped class
    private IEnumerable<StockData> open_value;
    private IEnumerable<StockData> close_value;
    private IEnumerable<StockData> low_value;
    private IEnumerable<StockData> high_value;
    private double max_reach90days;
    private double min_reach90days;
    private double max_reach60days;
    private double min_reach60days;
    private double max_reach30days;
    private double min_reach30days;
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
    private List<MinMaxData> basicValueList;
    private List<StockData> mid30daysList;
    private List<Mid30DaysData> mid30days_list;
    private List<VolatilityData> volatilitiesList;
    private PercentualReturnData percentualreturnData;
    private List<PercentualReturnData> percentualreturnList;
    private IEnumerable<StockData> elements7days;


    public generated_data(IStockDataService stockDataService, IConfiguration configuration)
    {
        _stockDataService = stockDataService;
    }

    
    public async Task<List<MinMaxData>> MinMaxDataValues()
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
        // max and min values in 3, 2 and 1 months
        max_reach90days = list_data.Max(x => x.Close);
        min_reach90days = list_data.Min(x => x.Close);

        max_reach60days = list_data
            .Where(x => x.Close > 1)
            .Take(60)
            .Max(x => x.Close);
        
        min_reach60days = list_data
            .Where(x => x.Close > 1)
            .Take(60)
            //.OrderDescending()
            .Min(x => x.Close);
        
        max_reach30days = list_data
            .Where(x => x.Close > 1)
            .Take(30)
           // .OrderDescending()
            .Max(x => x.Close);
        
        min_reach30days = list_data
            .Where(x => x.Close > 1)
            .Take(30)
            //.OrderDescending()
            .Min(x => x.Close);



        MinMaxData basicValue = new MinMaxData()
        {
            MaxReach90Days = max_reach90days,
            MinReach90Days = min_reach90days,
            MaxReach60Days = max_reach60days,
            MinReach60Days = min_reach60days,
            MaxReach30Days = max_reach30days,
            MinReach30Days = min_reach30days
        };
        basicValueList = new List<MinMaxData>();
        basicValueList.Add(basicValue);
        
        return basicValueList;
    }

    public async Task<List<StockData>> BasicValuesData()
    {
        throw new NotImplementedException();
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
        var loaderBasic = await MinMaxDataValues();
        var loaderData = await DateValuesData();
        
        List<StockData> list_data = await _stockDataService.GetStockData();
        
        // volatilities 
        volatility30Days = (max_reach90days - min_reach90days) / date_30days_count;
        volatility15Days = (max_reach90days - min_reach90days) / date_15days_count;
        volatility7Days = (max_reach90days - min_reach90days) / date_7days_count;
        volatility3Days = (max_reach90days - min_reach90days) / date_3days_count;
        
        
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
            Mid30Days = mid30days_list,
            BasicValuesData = basicValueList
            

        };
        
        //var loader = await _stockDataService.parametersStock();
        var fullDataList = new List<FullData>();
        fullDataList.Add(fullData);
        
        return fullDataList;
        
    }

    public async Task<IEnumerable<DailyAverageReturnData>> DailyAverageReturn()
    {

        var loaderStockData = await _stockDataService.GetStockData();
        var dailyAverageDate = loaderStockData
            .Where(daily => daily.Date >= DateTime.Today.AddYears(-1));

        var dailyAverageDateList = dailyAverageDate
            .OrderBy(r => r.Date)
            .ToList();
        
        var calcDailyAverageReturn1Y = dailyAverageDateList
            .Select((r, index) => new DailyAverageReturnData
            {
                Date = r.Date,
                Symbol = r.Symbol,
                PercentualDaily = index == 0
                    ? 0
                    : (r.Close - dailyAverageDateList[index - 1].Close) / dailyAverageDateList[index - 1].Close * 100
            })
            .Skip(1)
            .ToList();


        return calcDailyAverageReturn1Y;
    }
    public async Task<IEnumerable<DailyAverageReturnData>> WeeklyAverageReturn()
    {
        var loaderStockData = await _stockDataService.GetStockData();

        var weeklyAverageDateList = loaderStockData
            .Where(d => d.Date >= DateTime.Today.AddYears(-1))
            .OrderBy(d => d.Date)
            .ToList();

        var calcWeeklyAverageReturn1Y = weeklyAverageDateList
            .Select((r, index) =>
            {
                var last7Days = weeklyAverageDateList
                    .Skip(Math.Max(0, index - 7))
                    .Take(Math.Min(7, index))
                    .Select(x => x.Close);

                var avgLast7Days = last7Days.Any() ? last7Days.Average() : r.Close;

                var percentual = last7Days.Any() ? (r.Close - avgLast7Days) / avgLast7Days * 100 : 0;

                return new DailyAverageReturnData
                {
                    Date = r.Date,
                    Symbol = r.Symbol,
                    PercentualDaily = percentual
                };
            })
            .ToList();

        return calcWeeklyAverageReturn1Y;
    }

    public async Task<IEnumerable<DailyAverageReturnData>> MonthlyAverageReturn()
    {
        var loaderStockData = await _stockDataService.GetStockData();

        var monthlyAverageDateList = loaderStockData
            .Where(d => d.Date >= DateTime.Today.AddYears(-1))
            .OrderBy(d => d.Date)
            .ToList();

        var result = new List<DailyAverageReturnData>();
        int daysBack = 30;

        var calcMonthlyAverageReturn1Y = monthlyAverageDateList
            .Select((r, index) =>
            {
                var last30Days = monthlyAverageDateList
                    .Skip(Math.Max(0, index - daysBack))
                    .Take(Math.Min(daysBack, index))
                    .Select(x => x.Close);

                var avgLast30Days = last30Days.Any() ? last30Days.Average() : r.Close;

                var percentual = last30Days.Any() ? (r.Close - avgLast30Days) / avgLast30Days * 100 : 0;

                return new DailyAverageReturnData
                {
                    Date = r.Date,
                    Symbol = r.Symbol,
                    PercentualDaily = percentual
                };
            })
            .ToList();

        return calcMonthlyAverageReturn1Y;
    }

    public async Task<IEnumerable<DailyAverageReturnData>> YearlyAverageReturn()
    {
        var loaderStockData = await _stockDataService.GetStockData();

        var yearlyData = loaderStockData
            .Where(d => d.Date >= DateTime.Today.AddYears(-1))
            .OrderBy(d => d.Date)
            .ToList();

        if (!yearlyData.Any())
            return new List<DailyAverageReturnData>();

        var averageClose = yearlyData.Average(x => x.Close);
        var lastDay = yearlyData.Last();

        var percentual = (lastDay.Close - averageClose) / averageClose * 100;

        return new List<DailyAverageReturnData>
        {
            new DailyAverageReturnData
            {
                Date = lastDay.Date,
                Symbol = lastDay.Symbol,
                PercentualDaily = percentual
            }
        };
    }


    public async Task<List<StockData>> CumulativeReturnLastYear()
    {
        throw  new NotImplementedException();
    }
    public async Task<List<StockData>> CumulativeReturnLastMonth()
    {
        throw  new NotImplementedException();
    }
    public async Task<List<StockData>> SharpeRatio()
    {
        throw  new NotImplementedException();
    }
    public async Task<List<StockData>> AnnualizedReturn()
    {
        throw  new NotImplementedException();
    }
    public async Task<List<StockData>> MaxDrawdown()
    {
        throw  new NotImplementedException();
    }
    
    
    
    
}
