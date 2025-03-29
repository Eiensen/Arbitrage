namespace DataCollectorService.WebApi.Models;

public class BinanceResponse
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
