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
    public readonly IConfiguration _configuration;

    
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
    private int date_7days_count;
    private int date_3days_count;
    private double volatility30Days;
    private double percentualDaily;
    private double percentual7Days;
    private double percentual14Days;
    private double percentual30Days;
    private double percentual60Days;
    private double percentual90Days;
    private string positiveMid30days;
    private string negativeMid30days;
    private string brokenMid30days;
  


    public generated_data(IStockDataService stockDataService, IConfiguration configuration)
    {
        _stockDataService = stockDataService;
        _configuration = configuration;
    }



    public async Task<string> stock_calculus_base()
    {

        var init_charger = await _stockDataService.parametersStock();
        List<StockData> list_data = _stockDataService.GetStockData();

        open_value = list_data.Where(x => x.Open > 1);
        close_value = list_data.Where(x => x.Close > 1);

        low_value = list_data.Where(x => x.Low > 1);
        high_value = list_data.Where(x => x.High > 1);

        /*if (!close_value.Any())
        {
            throw new Exception("close_value null");;
        }*/
        // max and min values in 3 months
        var max_reach = list_data.Max(x => x.Close);
        var min_reach = list_data.Min(x => x.Close);

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


        // closes
        var close_daily = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(1)
            .Select(x => x.Close);

        var close_yesterday = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Skip(1)
            .Take(1)
            .Select(x => x.Close);

        var close_weekly = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(7)
            .Select(x => x.Close);

        var close_14days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(14)
            .Select(x => x.Close);

        var close_30days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(30)
            .Select(x => x.Close);

        var close_60days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(60)
            .Select(x => x.Close);

        var close_90days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(90)
            .Select(x => x.Close);


        // volatilities 
        volatility30Days = (max_reach - min_reach) / date_30days_count;
        var volatility_15days = (max_reach - min_reach) / date_15days_count;
        var volatility_7days = (max_reach - min_reach) / date_7days_count;
        var volatility_3days = (max_reach - min_reach) / date_3days_count;

        // percentual return in x period
        // close date_x - close date_y / close date_y * 100
        percentualDaily = (close_daily.First() - close_yesterday.Last()) / close_yesterday.Last() * 100;
        percentual7Days = (close_daily.First() - close_weekly.Last()) / close_weekly.Last() * 100;
        percentual14Days = (close_daily.First() - close_14days.Last()) / close_14days.Last() * 100;
        percentual30Days = (close_daily.First() - close_30days.Last()) / close_30days.Last() * 100;
        percentual60Days = (close_daily.First() - close_60days.Last()) / close_60days.Last() * 100;
        percentual90Days = (close_daily.First() - close_90days.Last()) / close_90days.Last() * 100;

        // 30 last days mid 

        var mid30days = list_data
            .OrderByDescending(x => x.Date)
            .Where(x => x.Close > 1)
            .Take(30)
            .Average(x => x.Close);

        var mid30days_value = close_daily.First() - mid30days;

        positiveMid30days = $"Positive status: {mid30days_value}";
        negativeMid30days = $"Negative status: {mid30days_value}";
        brokenMid30days = $"Broken status: {mid30days_value}";
        
        
        // all list item
        List<GeneralCalculusData> volatilities_list = new List<GeneralCalculusData>();

        GeneralCalculusData volatilityData = new GeneralCalculusData
        {
            Volatility3Days = volatility_3days,
            Volatility7Days = volatility_7days,
            Volatility15Days = volatility_15days,
            Volatility30Days = volatility30Days
        };
        
        volatilities_list.Add(volatilityData);
        var stringBuilder = new StringBuilder();
        foreach (var item in volatilities_list)
        {
            stringBuilder.AppendLine(
                $"Volatility 3 days: {item.Volatility3Days}" + "\n" +
                $"Volatility 7 days: {item.Volatility7Days}" + "\n" +
                $"Volatility 15 days:  {item.Volatility15Days}" + "\n" +
                $"Volatility 30 days:  {item.Volatility30Days}"
            );
        }
        

        return stringBuilder.ToString();

    }
    
}
