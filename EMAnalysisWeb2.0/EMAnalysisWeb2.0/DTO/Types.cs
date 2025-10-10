namespace EMAnalysisWeb.DTO;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeAvgSwitch
{
    [EnumMember(Value = "DAILY RETURN")]
    Daily,

    [EnumMember(Value = "WEEKLY RETURN")]
    Weekly,

    [EnumMember(Value = "MONTHLY RETURN")]
    Monthly,

    [EnumMember(Value = "YEARLY RETURN")]
    Yearly
}