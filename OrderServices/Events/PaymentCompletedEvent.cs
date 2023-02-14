namespace OrderServices.Events;

public class PaymentCompletedEvent
{
    public Guid OrderId { get; set; }
}