using GameDealFinder.Models;
using GameDealFinder.Models.Dtos;

namespace GameDealFinder.Mappers;

public static class CheapSharkMapper
{
    public static GameDeal ToGameDeal(CheapSharkGameDto dto) => new()
    {
        GameId = dto.GameId ?? string.Empty,
        Title = dto.Title ?? string.Empty,
        CheapestPrice = dto.Cheapest,
        ThumbnailUrl = dto.Thumb
    };
}