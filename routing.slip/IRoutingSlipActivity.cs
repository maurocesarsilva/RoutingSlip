namespace routing.slip
{
    internal interface IRoutingSlipActivity<T>  where T : IParam
    {
        Task ExecuteAsync(T obj);
        Task CompensateAsync(T obj);
    }
}
