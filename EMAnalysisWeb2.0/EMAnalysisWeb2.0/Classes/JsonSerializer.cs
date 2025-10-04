using System.Text.Json.Serialization;
using EMAnalysisWeb.DTO;

namespace EMAnalysisWeb.Classes;


//DTO JSON SERIALIZER HEREEEEE
[JsonSerializable(typeof(List<VolatilityData>))]
[JsonSerializable(typeof(List<PercentualReturnData>))]
[JsonSerializable(typeof(List<Mid30DaysData>))]
[JsonSerializable(typeof(List<Mid30DaysDataInfo>))]
[JsonSerializable(typeof(List<FullData>))]
[JsonSerializable(typeof(List<MinMaxData>))]
public partial class AppJsonContext : JsonSerializerContext { }