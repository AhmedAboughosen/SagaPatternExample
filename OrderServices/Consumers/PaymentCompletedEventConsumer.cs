using EasyNetQ.AutoSubscribe;
using OrderServices.Events;
using OrderServices.Handler;

namespace OrderServices.Consumers;

public class PaymentCompletedEventConsumer : IConsumeAsync<PaymentCompletedEvent>
{
    private readonly OrderHandler _orderHandler;

    public PaymentCompletedEventConsumer(OrderHandler orderHandler)
    {
        _orderHandler = orderHandler;
    }


    public async Task ConsumeAsync(PaymentCompletedEvent message, CancellationToken cancellationToken = default)
    {
        await _orderHandler.CompleteOrderAsync(message.OrderId);
    }
}