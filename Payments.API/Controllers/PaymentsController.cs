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
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePaymentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("total-payments")]
        public async Task<IActionResult> GetTotalPayment([FromQuery] TotalPaymentQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("state-payment-summaries")]
        public async Task<IActionResult> GetStatePaymentSummaries([FromQuery] StatePaymentSummariesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("state-payment-report")]
        public async Task<IActionResult> GetStatePaymentReport([FromQuery] StatePaymentReportQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("state-grossIncome-report")]
        public async Task<IActionResult> GetStateGrossIncomeReports([FromQuery] StateGrossIncomeReportQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
