namespace OrderServices.Events;

public class PaymentRejectedEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; }

}