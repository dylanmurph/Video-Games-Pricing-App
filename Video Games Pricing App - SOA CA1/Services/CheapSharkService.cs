using System.Net.Http.Json;
using System.Text.Json;
using GameDealFinder.Exceptions;
using GameDealFinder.Mappers;
using GameDealFinder.Models;
using GameDealFinder.Models.Dtos;

namespace GameDealFinder.Services;

public class CheapSharkService : ICheapSharkService
{
    private const int PageSize = 5;

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

        var url = $"games?title={Uri.EscapeDataString(title.Trim())}&limit={PageSize}";

        try
        {
            var response = await _http.GetFromJsonAsync<List<CheapSharkGameDto>>(url);
            return response?.Select(CheapSharkMapper.ToGameDeal).ToList() ?? new List<GameDeal>();
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