using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using OrderServices.Events;
using PaymentService.Handler;

namespace PaymentService.Consumers;

public class StocksReservedEventConsumer : IConsumeAsync<StocksReservedEvent>
{
    private readonly PaymentHandler _paymentService;
    private readonly IBus _bus;

    public StocksReservedEventConsumer(PaymentHandler paymentService, IBus bus)
    {
        _paymentService = paymentService;
        _bus = bus;
    }

    public async Task ConsumeAsync(StocksReservedEvent message, CancellationToken cancellationToken = default)
    {   
        Tuple<bool, string> isPaymentCompleted = await _paymentService.DoPaymentAsync(message.WalletId, message.UserId, message.TotalAmount);

        if (isPaymentCompleted.Item1)
        {
            await _bus.PubSub.PublishAsync(new PaymentCompletedEvent
            {
                OrderId = message.OrderId
            });
        }
        else
        {
            await _bus.PubSub.PublishAsync(new PaymentRejectedEvent
            {
                OrderId = message.OrderId,
                Reason = isPaymentCompleted.Item2
            });
        }
    }
}