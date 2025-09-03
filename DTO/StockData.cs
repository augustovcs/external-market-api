namespace api_external_scrapper.DTO;

public class StockData
{
    public double AdjustedClose { get; set; }
    public string Date { get; set; }
    public string Symbol { get; set; }
    
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    
    public double DividendAmount { get; set; }
    public int Volume { get; set; }
}