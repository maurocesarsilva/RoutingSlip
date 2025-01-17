namespace routing.slip
{
    class RoutingSlipActivityWrapper(Func<IParam, Task> execute, Func<IParam, Task> compensate)
    {
        public Task ExecuteAsync(IParam param) => execute(param);
        public Task CompensateAsync(IParam param) => compensate(param);
    }

    internal class RoutingSlip
    {
        private readonly Dictionary<RoutingSlipActivityWrapper, IParam> _activities;
        private readonly Dictionary<RoutingSlipActivityWrapper, IParam> _executedActivities;

        public RoutingSlip()
        {
            _activities = [];
            _executedActivities = [];
        }
        public void AddActivity<T>(IRoutingSlipActivity<T> activity, IParam obj) where T : IParam
        {
            var wrapper = new RoutingSlipActivityWrapper
                (
                   param => activity.ExecuteAsync((T)param),
                   param => activity.CompensateAsync((T)param)
                );

            _activities.Add(wrapper, obj);
        }
        public async Task ExecuteAsync()
        {
            try
            {
                foreach (var activity in _activities)
                {
                    Console.WriteLine($"Starting {activity.Key.GetType().Name}...");
                    _executedActivities.Add(activity.Key, activity.Value);
                    await activity.Key.ExecuteAsync(activity.Value);
                }

                Console.WriteLine("Routing Slip completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during execution: {ex.Message}");
                await CompensateAsync(); // Realiza o rollback
            }
        }
        private async Task CompensateAsync()
        {
            Console.WriteLine("Compensating activities...");
            foreach (var activity in _executedActivities)
            {
                await activity.Key.CompensateAsync(activity.Value);
            }
        }
    }
}
