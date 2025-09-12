using System.Text.Json.Serialization;

namespace api_external_scrapper.DTO;

public class GeneralCalculusData
{
    // VOLATILITY
    
    public double Volatility3Days { get; set; }
    public double Volatility7Days { get; set; }
    public double Volatility15Days { get; set; }
    public double Volatility30Days { get; set; }
    
   
}

public class PercentualReturnData
{
    //Percentual return in x period
    public double PercentualReturn { get; set; }
    public double PercentualReturn7Days { get; set; }
    public double PercentualReturn14Days { get; set; }
    public double PercentualReturn30Days { get; set; }
    public double PercentualReturn60Days { get; set; }
    public double PercentualReturn90Days { get; set; }

}

//last 30 days mid 

public class Mid30DaysData
{
    
    public double Mid30Days { get; set; }
    
    
    
}

public class Mid30DaysDataInfo : Mid30DaysData
{
    public double CloseDaily { get; set; }
    public List<StockData> Last30Days { get; set; }
    
    
}

// closes

public class Closes
{
    
}
    
// last x days


public class LastXDays
{
    
}