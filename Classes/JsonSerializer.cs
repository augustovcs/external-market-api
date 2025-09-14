using System.Text.Json.Serialization;
using api_external_scrapper.DTO;

namespace api_external_scrapper.Classes;


//DTO JSON SERIALIZER HEREEEEE
[JsonSerializable(typeof(List<VolatilityData>))]
[JsonSerializable(typeof(List<PercentualReturnData>))]
[JsonSerializable(typeof(List<Mid30DaysData>))]
[JsonSerializable(typeof(List<Mid30DaysDataInfo>))]
[JsonSerializable(typeof(List<FullData>))]
public partial class AppJsonContext : JsonSerializerContext { }