namespace api_external_scrapper.DTO;

/*public class ClosesStockData
{
    public double Close { get; set; }

}*/

public class StockData  // :  ClosesStockData
{
    //public double AdjustedClose { get; set; }
    public DateTime Date { get; set; }
    public string Symbol { get; set; }
    
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    //public double DividendAmount { get; set; }
    public int Volume { get; set; }
}

public class MinMaxData
{
    public double MaxReach90Days { get; set; }
    public double MinReach90Days { get; set; }
    public double MaxReach60Days { get; set; }
    public double MinReach60Days { get; set; }
    public double MaxReach30Days { get; set; }
    public double MinReach30Days { get; set; }
}
