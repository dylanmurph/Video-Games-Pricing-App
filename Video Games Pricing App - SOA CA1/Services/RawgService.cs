using GameDealFinder.Exceptions;
using GameDealFinder.Mappers;
using GameDealFinder.Models;
using GameDealFinder.Models.Dtos;

namespace GameDealFinder.Services;

public class RawgService : IRawgService
{
    private const int PageSize = 20;

    private readonly HttpClient _http;
    private readonly string? _apiKey;

    public RawgService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _apiKey = configuration["RawgApiKey"];
    }

    public async Task<List<Game>> SearchGamesAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return new List<Game>();
        }

        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            throw new ApiException("RAWG API key is missing. Add RawgApiKey to user secrets.");
        }

        var url = $"games?key={_apiKey}&search={Uri.EscapeDataString(query.Trim())}" +
          $"&search_precise=true&exclude_additions=true&page_size={PageSize}";

        try
        {
            var response = await _http.GetFromJsonAsync<RawgSearchResponseDto>(url);
            return response?.Results?.Select(RawgMapper.ToGame).ToList() ?? new List<Game>();
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("Couldn't get games from RAWG. Please try again.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new ApiException("RAWG took too long to respond.", ex);
        }
    }
}