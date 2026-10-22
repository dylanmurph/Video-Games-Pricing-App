namespace GameDealFinder.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly? Released { get; set; }
    public double Rating { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }
}