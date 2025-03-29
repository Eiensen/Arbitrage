namespace DataCollectorService.WebApi.Services;

public interface IBinanceService
{
    Task<decimal> GetPriceAsync(string symbol);
}