using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace GameDealFinder.Components.Pages;

public partial class ApiTest
{
    [Inject] private IHttpClientFactory ClientFactory { get; set; } = default!;
    [Inject] private IConfiguration Configuration { get; set; } = default!;

    private static readonly JsonSerializerOptions PrettyPrint = new() { WriteIndented = true };

    private string title = "hades";
    private string rawgJson = "";
    private string cheapSharkJson = "";
    private bool rawgError;
    private bool cheapSharkError;
    private bool loading;

    private async Task LoadAsync()
    {
        loading = true;
        rawgError = false;
        cheapSharkError = false;

        var key = Configuration["RawgApiKey"];
        var query = Uri.EscapeDataString(title);

        var rawg = await GetJsonAsync($"https://api.rawg.io/api/games?key={key}&search={query}&page_size=3");
        rawgJson = rawg ?? "";
        rawgError = rawg is null;

        var cheapShark = await GetJsonAsync($"https://www.cheapshark.com/api/1.0/games?title={query}&limit=3");
        cheapSharkJson = cheapShark ?? "";
        cheapSharkError = cheapShark is null;

        loading = false;
    }

    private async Task<string?> GetJsonAsync(string url)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("User-Agent", "GameDealFinder");

        var client = ClientFactory.CreateClient();

        try
        {
            using var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(responseStream);
            return JsonSerializer.Serialize(document.RootElement, PrettyPrint);
        }
        catch (HttpRequestException)
        {
            return null;
        }
        catch (JsonException)
        {
            return null;
        }
    }
}