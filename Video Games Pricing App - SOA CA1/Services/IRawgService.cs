using GameDealFinder.Models;

namespace GameDealFinder.Services;

public interface IRawgService
{
    Task<List<Game>> SearchGamesAsync(string query);
}