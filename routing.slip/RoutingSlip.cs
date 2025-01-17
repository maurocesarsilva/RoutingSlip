namespace routing.slip
{
    internal class RoutingSlip
    {
        private readonly Dictionary<IRoutingSlipActivity, object> _activities;
        private readonly Dictionary<IRoutingSlipActivity, object> _executedActivities;

        public RoutingSlip()
        {
            _activities = [];
            _executedActivities = [];
        }
        public void AddActivity<T>(IRoutingSlipActivity activity, T obj) 
        {
            _activities.Add(activity, obj);
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
