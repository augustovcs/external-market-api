using System.Globalization;
using System.Net;
using api_external_scrapper.DTO;
using CsvHelper;

namespace api_external_scrapper.Services;

public class stockDataService
{
    private readonly IConfiguration _configuration;

    public stockDataService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void parametersStock()
    {
        double stockdata100days = 0;
        double stockdata50days = 0;
        double stockdata20days = 0;
        double stockdata10days = 0;

        int dayCount = 0;
        string printOutSpacer = "   ";

        string symbol = "IBM";

        string API_KEY = _configuration.GetValue<string>("API_ALPHA");
        string queryURL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={API_KEY}&datatype=csv";

        WebClient clientService = new WebClient();
        string data_client = clientService.DownloadString(queryURL);
        
        string dateTime = symbol + "-" + DateTime.Now.ToString("dd-MM-yyyy");
        
        //IMPLEMENT PREVENTION FOR MULTIPLE CALCULATIONS 
        // CHECK CSV CURRENTLY DATE ALREADY EXISTS.
        if (!File.Exists(@"/home/augustoviegascs/Documents/dotnet/api_external_scrapper" + dateTime + ".csv"))
        {
            File.WriteAllText(@"stockData.csv" + dateTime + ".csv", data_client);
        }

        using (StreamReader reader = new StreamReader(@"/home/augustoviegascs/Documents/dotnet/api_external_scrapper"))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            List<StockData> stockDataList = new List<StockData>();

            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                StockData stockData = new StockData
                {
                    AdjustedClose = csv.GetField<double>("AdjustedClose"),
                    Date = DateTime.Parse(csv.GetField<string>("timestamp")).ToString("MM/dd/yyyy"),
                    Symbol = symbol,
                    Open = csv.GetField<double>("Open"),
                    High = csv.GetField<double>("High"),
                    Low = csv.GetField<double>("Low"),
                    Close = csv.GetField<double>("Close"),
                    Volume = csv.GetField<int>("Volume"),
                    DividendAmount = csv.GetField<double>("dividendAmount"),
                };

                stockdata100days += stockData.AdjustedClose;
                dayCount++;


                if (dayCount == 20)
                {
                    stockdata20days = stockdata100days;
                }

                if (dayCount == 50)
                {
                    stockdata50days = stockdata100days;
                }
                
                stockDataList.Add(stockData);

                foreach (var item in stockDataList)
                {
                    Console.WriteLine($"{item.Open.ToString("0.00")}{printOutSpacer}");
                    
                }
                    
                
            }
        }
    }


}