namespace routing.slip
{
    internal class ActivityA : IRoutingSlipActivity
    {
        public async Task CompensateAsync<T>(T obj)
        {
            var objStr = obj as string;
            Console.WriteLine($"Activity {objStr}...");
            await Task.Delay(500);
        }

        public async Task ExecuteAsync<T>(T obj)
        {
            var objStr = obj as string;
            Console.WriteLine($"Compensating Activity {objStr}...");
            await Task.Delay(500);
        }
    }
}
