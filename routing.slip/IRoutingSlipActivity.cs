namespace routing.slip
{
    internal interface IRoutingSlipActivity
    {
        Task ExecuteAsync<T>(T obj);
        Task CompensateAsync<T>(T obj);
    }
}
