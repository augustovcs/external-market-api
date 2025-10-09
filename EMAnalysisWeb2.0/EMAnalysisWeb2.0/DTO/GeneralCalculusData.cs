using System.Text.Json.Serialization;

namespace EMAnalysisWeb.DTO;


public class VolatilityData
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


//  FULL DATA CALCULUS

public class FullData
{
    public List<VolatilityData> Full_Volatility { get; set; }
    public List<PercentualReturnData> Full_PercentualReturn { get; set; }
    public List<Mid30DaysData> Mid30Days { get; set; }
    public List<MinMaxData> BasicValuesData { get; set; }
}

public class DailyAverageReturnData
{
    public DateTime Date { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public double PercentualDaily { get; set; }
}