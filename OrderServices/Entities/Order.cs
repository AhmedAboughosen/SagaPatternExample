namespace OrderServices.Entities;

public class OrderModel
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public List<OrderItem> OrderItems  { get; set; }
    public int WalletId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderState State { get; set; }
}

public enum OrderState {Pending,Completed,Rejected}
public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}