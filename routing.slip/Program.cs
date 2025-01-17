using routing.slip;

await ActiveExecute();

static async Task ActiveExecute()
{
    var routingSlip = new RoutingSlip();
    routingSlip.AddActivity(new ActivityA(), new Param("Active A"));
    routingSlip.AddActivity(new ActivityB(), new Param("Active B"));
    await routingSlip.ExecuteAsync();
}

