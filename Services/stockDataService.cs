using System.Globalization;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using api_external_scrapper.DTO;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using api_external_scrapper.Interfaces;

namespace api_external_scrapper.Services;

public class stockDataService : IStockDataService
{
    private readonly IConfiguration _configuration;
    private StringBuilder stringBuilder;
    private string symbol = "IBM";
    public List<StockData> stockDataList { get; private set; }
    private string fileName_scrapping;
    private List<SymbolData> symbolDataList;
    private SymbolData symbolData;

    public stockDataService(IConfiguration configuration)
    {
        _configuration = configuration;
        stockDataList = new List<StockData>();
    }
    

    
public class IntWithCommasConverter : Int32Converter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;
        text = text.Replace(",", "");
        return base.ConvertFromString(text, row, memberMapData);
    }
}
    
public sealed class StockMap : ClassMap<StockData>
{
    public StockMap()
    {
        Map(m => m.Date).Name("timestamp");
        Map(m => m.Open).Name("open");
        Map(m => m.High).Name("high");
        Map(m => m.Low).Name("low");
        Map(m => m.Close).Name("close");
        Map(m => m.Volume).TypeConverter<IntWithCommasConverter>().Name("volume");
    }
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

    public string SaveStockListSymbol()
    {
        string API_KEY = _configuration.GetValue<string>("API_ALPHA");
        string queryURL = $"https://www.alphavantage.co/query?function=LISTING_STATUS&apikey={API_KEY}&datatype=json";
        
        WebClient clientService = new WebClient();
        
        string fileName = $"StockSymbolList.csv";
        string archiveDir = Path.Combine(Directory.GetCurrentDirectory());

        if (!Directory.Exists(archiveDir))
        {
            Directory.CreateDirectory(archiveDir);
        }
            
        string content_path = Path.Combine(archiveDir, fileName);
        
        if (!File.Exists(content_path))
        {
            File.WriteAllText(content_path, clientService.DownloadString(queryURL));
        }
        
        else
        {
            Console.WriteLine(" SYMBOL LIST ALREADY DOWNLOADED! :D ");
        }

        return content_path;
    }

    public string GenerateCSV()
    {
        symbolDataList = new List<SymbolData>();
        
        //Console.WriteLine(Directory.GetCurrentDirectory() + "/" + "StockSymbolList.csv");
        using (StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + "/" + "StockSymbolList.csv", Encoding.UTF8))
        using (CsvReader csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                symbolData = new SymbolData()
                {
                    Symbol = csv.GetField("symbol"),
                    Name = csv.GetField("name")
                    
                    
                };
        
                //Console.WriteLine($"{symbolData.Name} ({symbolData.Symbol})");
                
                symbolDataList.Add(symbolData);
            }
        }
        
        
        //test if the symbols are downloading
        //File.WriteAllLines("all_symbols.txt", symbolDataList.Select(s => $"{s.Name} {s.Symbol}"));
        
        
        fileName_scrapping = $"{symbolDataList[0].Symbol}";
        Console.WriteLine(fileName_scrapping);
        string dateTime_scrapping = DateTime.Now.ToString("yyyy-MM-dd");
        string archiveDir_scrapping = Path.Combine(Directory.GetCurrentDirectory(), $"Scrapping/StockData/{dateTime_scrapping}");
        
        //Console.WriteLine(fileName_scrapping);
        string content_scrapping = Path.Combine(archiveDir_scrapping, $"{fileName_scrapping}.csv");
        //Console.WriteLine(content_scrapping);
        
        if (!File.Exists(content_scrapping))
        {
            Console.WriteLine("INTERN API LOADED!!");
        }

        else
        {
            Console.WriteLine("INTERN API DATA DOWNLOADED OFFLINE!! NO API REQUEST NEEDED :)");
        }
        
        return content_scrapping;
    }

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
        
        //using (StreamReader reader = new StreamReader(SaveRawCSV()))
        using (StreamReader reader = new StreamReader(GenerateCSV()))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            
            csv.Context.RegisterClassMap<StockMap>();
            var records = csv.GetRecords<StockData>();
            foreach (var record in records)
            {
                StockData stockData = new StockData()
                {
                    Date = record.Date,
                    Open = record.Open,
                    High = record.High,
                    Low = record.Low,
                    Close = record.Close,
                    Volume = record.Volume,
                };
                
                stockDataList.Add(stockData);
            }
            
            /*csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                
                
                StockData stockData = new StockData
                {
                    
                    Date = DateTime.Parse(csv.GetField<string>("timestamp")),
                    //Symbol = symbol,
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
            }*/
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
        var gen_scrapper = GenerateCSV();
        return stockDataList;
    }
    
}