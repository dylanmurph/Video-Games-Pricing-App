namespace GameDealFinder.Models;

public class GameDetails : Game
{
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int? Metacritic { get; set; }
    public string? Website { get; set; }
    public List<string> Genres { get; set; } = new();
    public List<string> Platforms { get; set; } = new();
    public List<string> Developers { get; set; } = new();
}