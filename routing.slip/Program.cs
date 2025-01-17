using routing.slip;

await ActiveExecute();

static async Task ActiveExecute()
{
    var obj1 = new Param("metodo 1");
    var routingSlip = new RoutingSlip(Guid.NewGuid().ToString());

    routingSlip.AddActivity(() => metodoTeste1(obj1));
    routingSlip.AddActivity((() => metodoTeste2(obj1), () => metodoTesteCompensate(obj1)));

    await routingSlip.Execute();
}

static async Task metodoTeste1(Param param)
{
    Console.WriteLine("1");
}
static async Task metodoTeste2(Param param)
{
    Console.WriteLine("2");
    throw new NotImplementedException();
}
static async Task metodoTesteCompensate(Param param)
{
    Console.WriteLine("Compensate");
}


