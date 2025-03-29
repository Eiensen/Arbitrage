namespace DataCollectorService.WebApi.Models;

public class PriceUpdatedEvent
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime Timestamp { get; set; }
}
