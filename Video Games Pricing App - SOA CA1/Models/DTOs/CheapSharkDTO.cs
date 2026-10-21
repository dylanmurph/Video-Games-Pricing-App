using System.Text.Json.Serialization;

namespace GameDealFinder.Models.Dtos;
public class CheapSharkGameDto
{
    [JsonPropertyName("gameID")]
    public string? GameId { get; set; }

    [JsonPropertyName("external")]
    public string? Title { get; set; }

    [JsonPropertyName("cheapest")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal Cheapest { get; set; }

    [JsonPropertyName("thumb")]
    public string? Thumb { get; set; }
}