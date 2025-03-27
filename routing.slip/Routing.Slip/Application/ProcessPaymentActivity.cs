using MassTransit;

namespace Routing.Slip.Application;


public class ProcessPaymentActivity : IExecuteActivity<ProcessPaymentArguments>
{
    private readonly ILogger<ProcessPaymentActivity> _logger;

    public ProcessPaymentActivity(ILogger<ProcessPaymentActivity> logger)
    {
        _logger = logger;
    }

    public async Task<ExecutionResult> Execute(ExecuteContext<ProcessPaymentArguments> context)
    {
        _logger.LogInformation("💳 Processando pagamento para o pedido ID: {OrderId}", context.Arguments.OrderId);
        await Task.Delay(100); // Simula processamento

        throw new Exception("Teste Compensate");

        return context.Completed();
    }
}

public record ProcessPaymentArguments { public Guid OrderId { get; init; } }
