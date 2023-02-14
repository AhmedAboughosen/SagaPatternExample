using EasyNetQ.AutoSubscribe;
using OrderServices.Events;
using OrderServices.Handler;

namespace OrderServices.Consumers;

public class StocksReleasedEventConsumer : IConsumeAsync<StocksReleasedEvent>
{
    private readonly OrderHandler _orderHandler;

    public StocksReleasedEventConsumer(OrderHandler orderHandler)
    {
        _orderHandler = orderHandler;
    }


    public async Task ConsumeAsync(StocksReleasedEvent message, CancellationToken cancellationToken = default)
    {
        await _orderHandler.RejectOrderAsync(message.OrderId, message.Reason);
    }
}