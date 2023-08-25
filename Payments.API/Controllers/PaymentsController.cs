using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Domain.Commands;
using Payments.Domain.Query;

namespace Payments.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreatePaymentCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePaymentCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("total-payments")]
        public async Task<IActionResult> GetTotalPayment([FromQuery] TotalPaymentQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("state-payment-summaries")]
        public async Task<IActionResult> GetStatePaymentSummaries([FromQuery] StatePaymentSummariesQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("state-payment-report")]
        public async Task<IActionResult> GetStatePaymentReport([FromQuery] StatePaymentReportQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("state-grossIncome-report")]
        public async Task<IActionResult> GetStateGrossIncomeReports([FromQuery] StateGrossIncomeReportQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
