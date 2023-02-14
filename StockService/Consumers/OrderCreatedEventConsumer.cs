using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using OrderServices.Events;
using OrderServices.Handler;

namespace OrderServices.Consumers;

public class OrderCreatedEventConsumer : IConsumeAsync<OrderCreatedEvent>
{
    private readonly StockHandler _stockHandler;
    private readonly IBus _bus;


    public OrderCreatedEventConsumer(StockHandler stockHandler, IBus bus)
    {
        _stockHandler = stockHandler;
        _bus = bus;
    }

    public async Task ConsumeAsync(OrderCreatedEvent message, CancellationToken cancellationToken = default)
    {
        await _stockHandler.ReserveStocksAsync(message.OrderId);

  
        await _bus.PubSub.PublishAsync(new StocksReservedEvent
        {
            UserId = message.UserId,
            OrderId = message.OrderId,
            WalletId = message.WalletId,
            TotalAmount = message.TotalAmount
        });
    }
}