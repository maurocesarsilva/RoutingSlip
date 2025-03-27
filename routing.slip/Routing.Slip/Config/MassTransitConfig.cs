using MassTransit;
using Routing.Slip.Application;
using System.Reflection;

namespace Routing.Slip.Config
{
    public static class MassTransitConfig
    {
        public static void AddMassTransitWithRabbitMQ(this IServiceCollection services)
        {
            var entryAssembly = Assembly.GetEntryAssembly();

            services.AddMassTransit(busConfig =>
            {
                busConfig.SetKebabCaseEndpointNameFormatter();
                busConfig.AddConsumers(entryAssembly);

                //não tem compensação interface IExecuteActivity<IPaymentConfirmationArguments>
                //busConfig.AddExecuteActivity<ProcessPaymentConfirmationActivity, IPaymentConfirmationArguments>();

                busConfig.AddActivity<ValidateOrder1Activity, ValidateOrderArguments1, ValidateOrderLog1>(cfg =>
                  cfg.UseRetry(r =>
                      r.Exponential(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2)))
                  );


                busConfig.AddActivity<ValidateOrderActivity, ValidateOrderArguments, ValidateOrderLog>(cfg =>
                    cfg.UseRetry(r =>
                        r.Exponential(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2)))
                    );
                
                busConfig.AddExecuteActivity<ProcessPaymentActivity, ProcessPaymentArguments>(cfg =>
                    cfg.UseRetry(r =>
                        r.Exponential(2, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2)))
                    );

                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("10.61.32.6", "TesteMassTransit", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);

                });
            });
        }
    }
}
