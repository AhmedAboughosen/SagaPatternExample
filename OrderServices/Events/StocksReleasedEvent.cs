namespace OrderServices.Events;

public class StocksReleasedEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; }
}