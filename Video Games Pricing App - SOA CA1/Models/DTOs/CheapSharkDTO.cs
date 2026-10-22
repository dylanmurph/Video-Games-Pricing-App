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

    [JsonPropertyName("cheapestDealID")]
    public string? CheapestDealId { get; set; }
}

public class CheapSharkDealDto
{
    [JsonPropertyName("gameID")]
    public string? GameId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("dealID")]
    public string? DealId { get; set; }

    [JsonPropertyName("storeID")]
    public string? StoreId { get; set; }

    [JsonPropertyName("salePrice")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public decimal SalePrice { get; set; }

    [JsonPropertyName("thumb")]
    public string? Thumb { get; set; }
}

public class CheapSharkStoreDto
{
    [JsonPropertyName("storeID")]
    public string? StoreId { get; set; }

    [JsonPropertyName("storeName")]
    public string? StoreName { get; set; }
}