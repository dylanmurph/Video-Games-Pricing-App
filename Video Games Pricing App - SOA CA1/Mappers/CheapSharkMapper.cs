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
        ThumbnailUrl = dto.Thumb,
        CheapestDealId = dto.CheapestDealId ?? string.Empty
    };
    public static GameDeal ToGameDeal(CheapSharkDealDto dto, string storeName) => new()
    {
        GameId = dto.GameId ?? string.Empty,
        Title = dto.Title ?? string.Empty,
        CheapestPrice = dto.SalePrice,
        ThumbnailUrl = dto.Thumb,
        CheapestDealId = dto.DealId ?? string.Empty,
        StoreName = storeName
    };

    public static GameDeal ToGameDeal(CheapSharkStoreDealDto dto, string storeName) => new()
    {
        StoreName = storeName,
        CheapestPrice = dto.Price,
        CheapestDealId = dto.DealId ?? string.Empty
    };
}