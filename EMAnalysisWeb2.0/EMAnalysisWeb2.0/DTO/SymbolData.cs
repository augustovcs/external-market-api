namespace EMAnalysisWeb.DTO;

public class SymbolData
{
    
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float OverallRank { get; set; } // 1.0 to 10.0
    public float OverallRisk { get; set; }
    
}