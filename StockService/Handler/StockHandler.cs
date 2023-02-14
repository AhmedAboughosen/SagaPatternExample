using EasyNetQ;

namespace OrderServices.Handler;

public class StockHandler
{
    private readonly IBus _bus;

    
    public StockHandler(IBus bus)
    {
        _bus = bus;
    }

    public Task<bool> ReleaseStocksAsync(Guid orderId)
    {
        // Stock release logic

        return Task.FromResult(true);
    }

    public Task ReserveStocksAsync(Guid orderId)
    {
        // Stock reserve logic

        return Task.CompletedTask;
    }
}