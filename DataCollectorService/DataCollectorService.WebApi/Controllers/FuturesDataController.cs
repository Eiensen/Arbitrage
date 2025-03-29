using DataCollectorService.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataCollectorService.WebApi.Controllers;

[ApiController]
[Route("api/futures")]
public class FuturesDataController(
    IBinanceService binanceService
    ) : ControllerBase
{
    [HttpGet("{symbol}")]
    public async Task<ActionResult<decimal>> GetPrice(string symbol)
    {
        var price = await binanceService.GetPriceAsync(symbol);
        return Ok(price);
    }
}
