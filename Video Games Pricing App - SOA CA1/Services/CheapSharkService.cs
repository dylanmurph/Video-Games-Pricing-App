using System.Net.Http.Json;
using System.Text.Json;
using GameDealFinder.Exceptions;
using GameDealFinder.Mappers;
using GameDealFinder.Models;
using GameDealFinder.Models.Dtos;

namespace GameDealFinder.Services;

public class CheapSharkService : ICheapSharkService
{
    private const int PageSize = 150;

    private readonly HttpClient _http;

    public CheapSharkService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<GameDeal>> SearchDealsAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return new List<GameDeal>();
        }

        var url = $"deals?title={Uri.EscapeDataString(title.Trim())}&pageSize={PageSize}";

        try
        {
            var dealsTask = _http.GetFromJsonAsync<List<CheapSharkDealDto>>(url);
            var storesTask = _http.GetFromJsonAsync<List<CheapSharkStoreDto>>("stores");

            var deals = await dealsTask ?? new List<CheapSharkDealDto>();
            var stores = await storesTask ?? new List<CheapSharkStoreDto>();

            var storeNames = stores
                .Where(store => store.StoreId is not null && store.StoreName is not null)
                .ToDictionary(store => store.StoreId!, store => store.StoreName!);

            return deals
                .Select(deal => CheapSharkMapper.ToGameDeal(
                    deal,
                    storeNames.GetValueOrDefault(deal.StoreId ?? string.Empty, "Unknown store")))
                .ToList();
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("Couldn't get prices from CheapShark. Please try again.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new ApiException("CheapShark took too long to respond.", ex);
        }
    }
}