using System.Text.Json.Serialization;
using api_external_scrapper.DTO;

namespace api_external_scrapper.Classes;

[JsonSerializable(typeof(List<GeneralCalculusData>))]
[JsonSerializable(typeof(List<PercentualReturnData>))]
public partial class AppJsonContext : JsonSerializerContext { }