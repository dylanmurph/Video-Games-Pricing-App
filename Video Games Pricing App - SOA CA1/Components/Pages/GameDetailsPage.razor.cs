using GameDealFinder.Exceptions;
using GameDealFinder.Models;
using GameDealFinder.Services;
using Microsoft.AspNetCore.Components;

namespace GameDealFinder.Components.Pages;

public partial class GameDetailsPage
{
    [Parameter] public int Id { get; set; }

    [SupplyParameterFromQuery(Name = "deal")]
    public string? DealGameId { get; set; }

    [Inject] private IRawgService RawgService { get; set; } = default!;
    [Inject] private ICheapSharkService CheapSharkService { get; set; } = default!;

    private Game? game;
    private List<GameDeal> stores = new();
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            game = await RawgService.GetGameAsync(Id);

            if (!string.IsNullOrEmpty(DealGameId))
            {
                stores = await CheapSharkService.GetStorePricesAsync(DealGameId);
            }
        }
        catch (ApiException ex)
        {
            errorMessage = ex.Message;
        }
    }
}