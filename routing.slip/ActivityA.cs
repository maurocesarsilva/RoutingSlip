namespace routing.slip
{
    internal class ActivityA : IRoutingSlipActivity<Param>
    {
        public async Task CompensateAsync(Param obj)
        {
            Console.WriteLine($"Activity {obj.Value}...");
            await Task.Delay(500);
        }

        public async Task ExecuteAsync(Param obj)
        {
            Console.WriteLine($"Compensating Activity {obj.Value}...");
            await Task.Delay(500);
        }
    }
}
