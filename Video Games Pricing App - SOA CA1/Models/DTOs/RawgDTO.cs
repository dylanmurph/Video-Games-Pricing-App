using System.Text.Json.Serialization;

namespace GameDealFinder.Models.Dtos;

public class RawgSearchResponseDto
{
    [JsonPropertyName("results")]
    public List<RawgGameDto>? Results { get; set; }
}

public class RawgGameDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("released")]
    public string? Released { get; set; }

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("background_image")]
    public string? BackgroundImage { get; set; }

    [JsonPropertyName("description_raw")]
    public string? Description { get; set; }
}