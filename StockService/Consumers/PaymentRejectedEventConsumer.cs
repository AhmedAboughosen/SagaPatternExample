using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using OrderServices.Events;
using OrderServices.Handler;

namespace OrderServices.Consumers;

public class PaymentRejectedEventConsumer : IConsumeAsync<PaymentRejectedEvent>
{
    private readonly StockHandler _stockHandler;
    private readonly IBus _bus;


    public PaymentRejectedEventConsumer(StockHandler stockHandler, IBus bus)
    {
        _stockHandler = stockHandler;
        _bus = bus;
    }

    public async Task ConsumeAsync(PaymentRejectedEvent message, CancellationToken cancellationToken = default)
    {
        await _stockHandler.ReleaseStocksAsync(message.OrderId);

        await _bus.PubSub.PublishAsync(new StocksReleasedEvent
        {
            OrderId = message.OrderId,
            Reason = message.Reason
        });    }
}