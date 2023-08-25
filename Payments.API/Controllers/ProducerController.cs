using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Domain.Interfaces.Messaging;
using Payments.Domain.Request;

namespace Payments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/producer")]
    public class ProducerController : Controller
    {
        private readonly ICreateClientQueue _clientQueue;
        private readonly ICreatePaymentQueue _paymentQueue;
        public ProducerController(ICreateClientQueue clientQueue, ICreatePaymentQueue paymentQueue)
        {
            _clientQueue = clientQueue;
            _paymentQueue = paymentQueue;
        }


        [HttpPost("create-client")]
        public async Task<IActionResult> SendCreateClient([FromBody] CreateClientMessage request)
        {
            return Ok(await _clientQueue.SendToQueue(request));
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> SendCreatePayment([FromBody] CreatePaymentMessage request)
        {
            return Ok(await _paymentQueue.SendToQueue(request));
        }

    }
}
