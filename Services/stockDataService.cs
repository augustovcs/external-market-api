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
    private string symbol = "IBM";
    public List<StockData> stockDataList { get; private set; }

    public stockDataService(IConfiguration configuration)
    {
        _configuration = configuration;
        stockDataList = new List<StockData>();
    }

    
    // to implement
    public string GetRawCSV(string symbol = "IBM")
    {
        
        string API_KEY = _configuration.GetValue<string>("API_ALPHA");
        string queryURL =
            $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={API_KEY}&datatype=csv&outputsize=full";
        
        WebClient clientService = new WebClient();
        string dataClient = clientService.DownloadString(queryURL);
        
        
        return dataClient;
        
    }

    //to implement
    public string SaveRawCSV(string symbol = "IBM")
    {
        
        string content = "";
        string dateTime = symbol + "-" + DateTime.Now.ToString("dd-MM-yyyy");
        string fileName = $"stockData-{dateTime}.csv";
        string archiveDir = Path.Combine(Directory.GetCurrentDirectory(), "data_archives");

        if (!Directory.Exists(archiveDir))
        {
            Directory.CreateDirectory(archiveDir);
        }

        content = Path.Combine(archiveDir, fileName);
        if (!File.Exists(content))
        {
            File.WriteAllText(content, GetRawCSV());
        }

        else
        {
            Console.WriteLine("API DATA DOWNLOADED OFFLINE!! NO API REQUEST NEEDED :)");
        }

        return content;
    }

    public List<StockData> ParsingRaw()
    {

        stockDataList = new List<StockData>();

        using (StreamReader reader = new StreamReader(SaveRawCSV()))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                StockData stockData = new StockData
                {
                    Date = DateTime.Parse(csv.GetField<string>("timestamp")),
                    Symbol = symbol,
                    Open = csv.GetField<double>("open"),
                    High = csv.GetField<double>("high"),
                    Low = csv.GetField<double>("low"),
                    Close = csv.GetField<double>("close"),
                    Volume = csv.GetField<int>("volume"),
                };
                
                DateTime limit = DateTime.Now.AddMonths(-60);
                if (stockData.Date >= limit && !stockDataList.Any(s => s.Date == stockData.Date))
                {
                    stockDataList.Add(stockData);
                }
            }
        }
        
        return stockDataList;
    }

    public List<StockData> parametersStock()
    {
        
        var response =  ParsingRaw();
        return response;

    }
    
    public async Task <List<StockData>> GetStockData()
    {
        var loader =  parametersStock();
        return stockDataList;
    }
    
}