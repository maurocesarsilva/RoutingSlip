namespace Routing.Slip.Application;

using MassTransit;
using Microsoft.Extensions.Logging;

public class OrderProcessor : IConsumer<ProcessOrder>
{
    private readonly ILogger<OrderProcessor> _logger;
    private readonly IBus _bus;
    private readonly IEndpointNameFormatter _endpointNameFormatter;

    public OrderProcessor(ILogger<OrderProcessor> logger, IBus bus, IEndpointNameFormatter endpointNameFormatter)
    {
        _logger = logger;
        _bus = bus;
        _endpointNameFormatter = endpointNameFormatter;
    }

    public async Task Consume(ConsumeContext<ProcessOrder> context)
    {
        var orderId = context.Message.OrderId;
        _logger.LogInformation("📦 Iniciando processamento do pedido ID: {OrderId}", orderId);

        var builder = new RoutingSlipBuilder(NewId.NextGuid());

        builder.AddActivity("ValidateOrder1", GetActivityAddress<ValidateOrder1Activity, ValidateOrderArguments1>(),
        new ValidateOrderArguments1 { OrderId = orderId });

        builder.AddActivity("ValidateOrder", GetActivityAddress<ValidateOrderActivity, ValidateOrderArguments>(),
            new ValidateOrderArguments { OrderId = orderId });

        builder.AddActivity("ProcessPayment", GetActivityAddress<ProcessPaymentActivity, ProcessPaymentArguments>(),
            new ProcessPaymentArguments { OrderId = orderId });

        await _bus.Execute(builder.Build());
    }

    private Uri GetActivityAddress<TActivity, TArguments>()
         where TActivity : class, IExecuteActivity<TArguments>
         where TArguments : class
    {
        var name = _endpointNameFormatter.ExecuteActivity<TActivity, TArguments>();
        return new Uri($"exchange:{name}");
    }
}

public record ProcessOrder { public Guid OrderId { get; init; } }

