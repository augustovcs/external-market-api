using System.Text.Json.Serialization;
using api_external_scrapper.DTO;

namespace api_external_scrapper.Classes;

[JsonSerializable(typeof(List<GeneralCalculusData>))]
[JsonSerializable(typeof(List<PercentualReturnData>))]
[JsonSerializable(typeof(List<Mid30DaysData>))]
[JsonSerializable(typeof(List<Mid30DaysDataInfo>))]
public partial class AppJsonContext : JsonSerializerContext { }