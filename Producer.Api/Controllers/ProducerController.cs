using Contracts;
using MassTransit.ActiveMqTransport;
using MessagingServices;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _producerService;
        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        // POST api/producer
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] DemoMessageContract demoMessageContract)
        {
            if (string.IsNullOrEmpty(demoMessageContract.Message))
                return BadRequest("Message cannot be null or empty");

            try
            {
                await _producerService.SendMessageAsync(demoMessageContract);
            }
            catch (ActiveMqConnectionException)
            {
                return BadRequest("Could not connect to the Broker");
            }
            return Ok();
        }
    }
}
