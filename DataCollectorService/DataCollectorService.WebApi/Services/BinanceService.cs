using DataCollectorService.WebApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DataCollectorService.WebApi.Services;

public class BinanceService(
    HttpClient httpClient,
    IDistributedCache cache
    //IRabbitMqProducer rabbitMq
    ) : IBinanceService
{
    public async Task<decimal> GetPriceAsync(string symbol)
    {
        BinanceResponse? response;

        var symbolString = await cache.GetStringAsync(symbol.ToString());

        if (symbolString != null)
            response = JsonSerializer.Deserialize<BinanceResponse>(symbolString);
        else
            response = await httpClient.GetFromJsonAsync<BinanceResponse>(
                $"/api/v3/ticker/price?symbol={symbol}");

        if (response == null)
            throw new Exception();

        symbolString = JsonSerializer.Serialize(response);


        await cache.SetStringAsync(symbol.ToString(), symbolString, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        });

        //rabbitMq.Publish("PriceUpdated", new
        //{
        //    Symbol = symbol,
        //    Price = response.Price,
        //    Timestamp = DateTime.UtcNow
        //});

        return response.Price;

    }
}
