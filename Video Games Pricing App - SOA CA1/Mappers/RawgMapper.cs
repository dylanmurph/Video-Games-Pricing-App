using System.Globalization;
using GameDealFinder.Models;
using GameDealFinder.Models.Dtos;

namespace GameDealFinder.Mappers;

public static class RawgMapper
{
    public static Game ToGame(RawgGameDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name ?? string.Empty,
        Released = DateOnly.TryParse(dto.Released, CultureInfo.InvariantCulture, out var date) ? date : null,
        Rating = dto.Rating,
        ImageUrl = dto.BackgroundImage,
        Description = dto.Description
    };
}