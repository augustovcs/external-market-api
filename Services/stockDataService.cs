using System.Net;

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
        File.WriteAllText(@"stockData.csv" + dateTime + ".csv", data_client);
        
    }


}