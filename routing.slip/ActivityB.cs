namespace routing.slip
{
    internal class ActivityB : IRoutingSlipActivity
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

            throw new Exception("teste compensação");

            Console.WriteLine($"Compensating Activity {objStr}...");
            await Task.Delay(500);
        }
    }
}
