namespace OrderServices.Events;

public class OrderCreatedEvent
{
    public int UserId { get; set; }
    public Guid OrderId { get; set; }
    public int WalletId { get; set; }
    public decimal TotalAmount { get; set; }
}