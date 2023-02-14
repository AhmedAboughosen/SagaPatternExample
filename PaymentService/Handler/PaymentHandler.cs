using EasyNetQ;

namespace PaymentService.Handler;

public class PaymentHandler
{
    private readonly IBus _bus;

    
    public PaymentHandler(IBus bus)
    {
        _bus = bus;
    }

    public Task<Tuple<bool, string>> DoPaymentAsync(int walletId, int userId, decimal totalAmount)
    {
        throw new NotImplementedException();
    }
}