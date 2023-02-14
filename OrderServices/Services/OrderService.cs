using Grpc.Core;
using OrderServices.Handler;
using OrderServices.Model;

namespace OrderServices.Services;

public class OrderService : Order.OrderBase
{
    private readonly ILogger<OrderService> _logger;
    private readonly OrderHandler _orderHandler;

    public OrderService(ILogger<OrderService> logger, OrderHandler orderHandler)
    {
        _logger = logger;
        _orderHandler = orderHandler;
    }


    public override async Task<PlaceOrderReply> PlaceOrder(PlaceOrderRequest request, ServerCallContext context)
    {
      await  _orderHandler.CreateOrderAsync(new CreateOrderRequest
        {
            TotalAmount = decimal.Parse(request.TotalAmount) 
        });

      return new PlaceOrderReply
      {
          Message = "wait"
      };
    }
}