using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using OrderServices;
using OrderServices.Consumers;
using StockService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var bus = RabbitHutch.CreateBus(builder.Configuration["RabbitMQ:ConnectionString"]);

builder.Services.AddSingleton<IBus>(bus);
builder.Services.AddSingleton<MessageDispatcher>();
builder.Services.AddSingleton<AutoSubscriber>(_ =>
{
    return new AutoSubscriber(_.GetRequiredService<IBus>(), Assembly.GetExecutingAssembly().GetName().Name)
    {
        AutoSubscriberMessageDispatcher = _.GetRequiredService<MessageDispatcher>()
    };
});

builder.Services.AddScoped<OrderCreatedEventConsumer>();
builder.Services.AddScoped<PaymentRejectedEventConsumer>();

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();