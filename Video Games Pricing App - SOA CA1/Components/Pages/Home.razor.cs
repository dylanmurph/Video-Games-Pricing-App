using GameDealFinder.Exceptions;
using GameDealFinder.Models;
using GameDealFinder.Services;
using Microsoft.AspNetCore.Components;

namespace GameDealFinder.Components.Pages;

public partial class Home
{
    [Inject] private IRawgService RawgService { get; set; } = default!;
    [Inject] private ICheapSharkService CheapSharkService { get; set; } = default!;

    private string query = string.Empty;
    private List<Game>? games;
    private List<GameDeal> deals = new();
    private bool loading;
    private string? errorMessage;
    private string? priceWarning;

    private async Task SearchAsync()
    {
        errorMessage = null;
        priceWarning = null;

        if (string.IsNullOrWhiteSpace(query))
        {
            games = null;
            errorMessage = "Enter a game title to search.";
            return;
        }

        loading = true;

        var gamesTask = RawgService.SearchGamesAsync(query);
        var dealsTask = CheapSharkService.SearchDealsAsync(query);

        try
        {
            games = (await gamesTask)
            .OrderBy(game => game.Released ?? DateOnly.MaxValue)
            .ToList();
            deals = await GetDealsAsync(dealsTask);
        }
        catch (ApiException ex)
        {
            games = null;
            errorMessage = ex.Message;
        }
        finally
        {
            loading = false;
        }
    }

    private async Task<List<GameDeal>> GetDealsAsync(Task<List<GameDeal>> dealsTask)
    {
        try
        {
            return await dealsTask;
        }
        catch (ApiException ex)
        {
            priceWarning = $"Prices are unavailable right now. {ex.Message}";
            return new List<GameDeal>();
        }
    }

    private GameDeal? FindDeal(Game game)
    {
        var name = Clean(game.Name);

        return deals
            .Where(deal => Clean(deal.Title).StartsWith(name))
            .OrderBy(deal => deal.Title.Length)
            .ThenBy(deal => deal.CheapestPrice)
            .FirstOrDefault();
    }

    private static string Clean(string text) =>
        new string(text.ToLowerInvariant().Where(char.IsLetterOrDigit).ToArray());
}