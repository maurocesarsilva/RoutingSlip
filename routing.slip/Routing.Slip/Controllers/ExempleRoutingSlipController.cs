using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Routing.Slip.Application;

namespace Routing.Slip.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExempleRoutingSlipController(IBus _bus) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var orderId = Guid.NewGuid();
            await _bus.Publish(new ProcessOrder { OrderId = orderId });

            return Ok();
        }
    }
}
