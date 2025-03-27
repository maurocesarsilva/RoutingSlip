using MassTransit;

namespace Routing.Slip.Application;

public class ValidateOrder1Activity : IActivity<ValidateOrderArguments1, ValidateOrderLog1>
{
    private readonly ILogger<ValidateOrder1Activity> _logger;

    public ValidateOrder1Activity(ILogger<ValidateOrder1Activity> logger)
    {
        _logger = logger;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<ValidateOrderArguments1> context)
    {
        _logger.LogInformation("✅ Validando pedido ID: {OrderId}", context.Arguments.OrderId);
        await Task.Delay(1000); // Simula uma validação

        return context.Completed(new ValidateOrderLog { OrderId = context.Arguments.OrderId });
    }

    public async Task<CompensationResult> Compensate(CompensateContext<ValidateOrderLog1> context)
    {
        _logger.LogWarning("❌ Desfazendo validação do pedido ID: {OrderId}", context.Log.OrderId);
        return context.Compensated();
    }
}

public record ValidateOrderArguments1 { public Guid OrderId { get; init; } }
public record ValidateOrderLog1 { public Guid OrderId { get; init; } }
