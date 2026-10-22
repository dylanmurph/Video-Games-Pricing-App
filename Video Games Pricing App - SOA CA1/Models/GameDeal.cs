namespace GameDealFinder.Models;

public class GameDeal
{
    public string GameId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public decimal CheapestPrice { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string CheapestDealId { get; set; } = string.Empty;
    public string DealUrl => $"https://www.cheapshark.com/redirect?dealID={CheapestDealId}";
}