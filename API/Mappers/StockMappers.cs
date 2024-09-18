using API.DTOs.Stock;
using API.Models;
using System.Xml.Linq;

namespace API.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock StockModel)
        {
            return new StockDto
            {
                Id = StockModel.Id,
                CompanyName = StockModel.CompanyName,
                Industry = StockModel.Industry,
                LastDiv = StockModel.LastDiv,
                MarketCap = StockModel.MarketCap,
                Purchase = StockModel.Purchase,
                Symbol = StockModel.Symbol,
                Comments  = StockModel.Comments.Select(c => c.ToCommmentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto requestDto)
        {
            return new Stock
            {
                CompanyName = requestDto.CompanyName,
                Industry = requestDto.Industry,
                LastDiv = requestDto.LastDiv,
                MarketCap = requestDto.MarketCap,
                Purchase = requestDto.Purchase,
                Symbol = requestDto.Symbol
            };
        }
    }
}
