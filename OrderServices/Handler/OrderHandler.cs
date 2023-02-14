using EasyNetQ;
using OrderServices.Entities;
using OrderServices.Events;
using OrderServices.Model;

namespace OrderServices.Handler;

public class OrderHandler
{
    private readonly IBus _bus;

    public  List<OrderModel> _list = new List<OrderModel>();
    
    public OrderHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task CreateOrderAsync(CreateOrderRequest request)
    {
        // Order creation logic in "Pending" state.
        var id = Guid.NewGuid();
        
        _list.Add(new OrderModel
        {
            TotalAmount = request.TotalAmount,
            State = OrderState.Pending,
            Id = id
        });


        await _bus.PubSub.PublishAsync(new OrderCreatedEvent
        {
            UserId = 1,
            OrderId = id,
            WalletId = 1,
            TotalAmount = request.TotalAmount,
        });
    }

    public Task CompleteOrderAsync(Guid orderId)
    {
        // Change the order status as completed.

     var order =   _list.FirstOrDefault(o => o.Id == orderId);
     if (order != null)
     {
         order.State = OrderState.Completed;
         
     }
     
        return Task.CompletedTask;
    }

    public Task RejectOrderAsync(Guid orderId, string reason)
    {
        // Change the order status as rejected.
        var order =   _list.FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            order.State = OrderState.Rejected;
         
        }
        return Task.CompletedTask;
    }
}