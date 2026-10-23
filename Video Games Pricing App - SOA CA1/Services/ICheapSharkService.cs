using GameDealFinder.Models;

namespace GameDealFinder.Services;

public interface ICheapSharkService
{
    Task<List<GameDeal>> SearchDealsAsync(string title);
    Task<List<GameDeal>> GetStorePricesAsync(string gameId);
}