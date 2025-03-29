using DataCollectorService.WebApi.Services;
using Hangfire;

namespace DataCollectorService.WebApi.Jobs;

public class BinanceDataJob(
    IBinanceService binanceService
    )
{   
    [AutomaticRetry(Attempts = 3)]
    public async Task FetchPricesJob()
    {
        var symbols = new[] { "BTCUSDT_QUARTER", "BTCUSDT_BI-QUARTER" };
        foreach (var symbol in symbols)
        {
            await binanceService.GetPriceAsync(symbol);
        }
    }
}
