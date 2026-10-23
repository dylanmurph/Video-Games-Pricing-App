namespace GameDealFinder.Models;

public class GamePrices
{
    public List<GameDeal> Offers { get; set; } = new();
    public decimal? CheapestEver { get; set; }
}