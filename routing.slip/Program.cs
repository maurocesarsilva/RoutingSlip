using routing.slip;

await ActiveExecute();

async Task ActiveExecute()
{

    var routingSlip = new RoutingSlip();
    routingSlip.AddActivity(new ActivityA(), "Active A");
    routingSlip.AddActivity(new ActivityB(), "Active B");
    await routingSlip.ExecuteAsync();
}
