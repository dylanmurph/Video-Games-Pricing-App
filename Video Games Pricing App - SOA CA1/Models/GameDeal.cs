namespace GameDealFinder.Models;

public class GameDeal
{
    public string GameId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal CheapestPrice { get; set; }
    public string? ThumbnailUrl { get; set; }
}