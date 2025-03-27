using MassTransit;

namespace Routing.Slip.Application;

public class ValidateOrderActivity : IActivity<ValidateOrderArguments, ValidateOrderLog>
{
    private readonly ILogger<ValidateOrderActivity> _logger;

    public ValidateOrderActivity(ILogger<ValidateOrderActivity> logger)
    {
        _logger = logger;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<ValidateOrderArguments> context)
    {
        _logger.LogInformation("✅ Validando pedido ID: {OrderId}", context.Arguments.OrderId);
        await Task.Delay(1000); // Simula uma validação

        return context.Completed(new ValidateOrderLog { OrderId = context.Arguments.OrderId });
    }

    public async Task<CompensationResult> Compensate(CompensateContext<ValidateOrderLog> context)
    {
        _logger.LogWarning("❌ Desfazendo validação do pedido ID: {OrderId}", context.Log.OrderId);
        return context.Compensated();
    }
}

public record ValidateOrderArguments { public Guid OrderId { get; init; } }
public record ValidateOrderLog { public Guid OrderId { get; init; } }
