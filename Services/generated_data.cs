using System.Globalization;
using api_external_scrapper.DTO;
using api_external_scrapper.Services;
using CsvHelper;

namespace api_external_scrapper.Services;

public class generated_data
{
    public readonly stockDataService _stockDataService;
    public readonly IConfiguration _configuration;

    public generated_data(stockDataService stockDataService, IConfiguration configuration)
    {
        _stockDataService = stockDataService;
        _configuration = configuration;
    }

    public void stock_calculus_base()
    {
        
        List<StockData> list = _stockDataService.StockDataList;

        foreach (var stock in list)
        {
            Console.WriteLine($"Date: {stock.Date}");
        }
    





    }
}