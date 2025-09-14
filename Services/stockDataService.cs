using System.Globalization;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using api_external_scrapper.DTO;
using CsvHelper;
using api_external_scrapper.Interfaces;

namespace api_external_scrapper.Services;

public class stockDataService : IStockDataService
{
    private readonly IConfiguration _configuration;
    private StringBuilder stringBuilder;
    public List<StockData> stockDataList { get; private set; }

    public stockDataService(IConfiguration configuration)
    {
        _configuration = configuration;
        stockDataList = new List<StockData>();
    }



    public async Task<List<StockData>> parametersStock()
    {
        double stockdata100days = 0;
        double stockdata50days = 0;
        double stockdata20days = 0;
        double stockdata10days = 0;

        int dayCount = 0;
        string printOutSpacer = "   ";

        string symbol = "IBM";

        string API_KEY = _configuration.GetValue<string>("API_ALPHA");
        string queryURL =
            $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={API_KEY}&datatype=csv";

        WebClient clientService = new WebClient();
        string data_client = clientService.DownloadString(queryURL);
        if (!string.IsNullOrEmpty(data_client))
        {
            //RUN THE OFFLINE BASE API



        }


        string dateTime = symbol + "-" + DateTime.Now.ToString("dd-MM-yyyy");

        //IMPLEMENT PREVENTION FOR MULTIPLE CALCULATIONS 
        // CHECK CSV CURRENTLY DATE ALREADY EXISTS. ezzzzz
        string fileName = $"stockData-{dateTime}.csv";
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        string currentDir = Directory.GetCurrentDirectory();
        string archiveDir = Path.Combine(Directory.GetCurrentDirectory(), "data_archives");

        if (!Directory.Exists(archiveDir))
        {
            Directory.CreateDirectory(archiveDir);
        }

        string fullPath_archived = Path.Combine(archiveDir, fileName);

        if (!File.Exists(fullPath_archived + dateTime + ".csv"))
        {
            File.WriteAllText(fullPath_archived, data_client);
        }

        stockDataList = new List<StockData>();

        using (StreamReader reader = new StreamReader(fullPath_archived))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                StockData stockData = new StockData
                {
                    //AdjustedClose = csv.GetField<double>("adjusted_close"),
                    Date = DateTime.Parse(csv.GetField<string>("timestamp")),
                    Symbol = symbol,
                    Open = csv.GetField<double>("open"),
                    High = csv.GetField<double>("high"),
                    Low = csv.GetField<double>("low"),
                    Close = csv.GetField<double>("close"),
                    Volume = csv.GetField<int>("volume"),
                    //DividendAmount = csv.GetField<double>("DividendAmount"),
                };


                DateTime limit = DateTime.Now.AddMonths(-3);
                if (stockData.Date >= limit && !stockDataList.Any(s => s.Date == stockData.Date))
                {
                    
                    //????
                    stockDataList.Add(stockData);
                    //StockDataList.Add(stockData);

                }

            }

        }
        
        return stockDataList;

    }
    
    public async Task <List<StockData>> GetStockData()
    {
        return stockDataList;
    }
    
}