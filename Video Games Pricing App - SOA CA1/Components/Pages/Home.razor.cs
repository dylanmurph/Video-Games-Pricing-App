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
    private Dictionary<string, decimal> lowestPrices = new(StringComparer.OrdinalIgnoreCase);
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
            games = await gamesTask;
            lowestPrices = await GetLowestPricesAsync(dealsTask);
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

    private async Task<Dictionary<string, decimal>> GetLowestPricesAsync(Task<List<GameDeal>> dealsTask)
    {
        try
        {
            var deals = await dealsTask;

            return deals
                .GroupBy(deal => deal.Title, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    group => group.Key,
                    group => group.Min(deal => deal.CheapestPrice),
                    StringComparer.OrdinalIgnoreCase);
        }
        catch (ApiException ex)
        {
            priceWarning = $"Prices are unavailable right now. {ex.Message}";
            return new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        }
    }

    private string LowestPriceFor(Game game) =>
        lowestPrices.TryGetValue(game.Name, out var price) ? $"${price:0.00}" : "Not found";
}