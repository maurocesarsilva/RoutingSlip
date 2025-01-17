namespace routing.slip
{
    internal class ActivityB : IRoutingSlipActivity<Param>
    {
        public async Task CompensateAsync(Param obj)
        {
            Console.WriteLine($"Activity {obj.Value}...");
            await Task.Delay(500);
        }

        public async Task ExecuteAsync(Param obj)
        {
            throw new Exception("teste compensação");

            Console.WriteLine($"Compensating Activity {obj.Value}...");
            await Task.Delay(500);
        }
    }
}
