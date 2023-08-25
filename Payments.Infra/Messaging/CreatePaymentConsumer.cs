using MassTransit;
using MediatR;
using Payments.Domain.Commands;
using Payments.Domain.Request;

namespace Payments.Infra.Messaging
{
    public class CreatePaymentConsumer : IConsumer<CreatePaymentMessage>
    {
        private readonly IMediator _mediator;
        public CreatePaymentConsumer(IMediator mediator) 
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<CreatePaymentMessage> context)
        {
            var message = context.Message;
            var command = new CreatePaymentCommand(message.ContractNumber, 
                                                   message.Quota, 
                                                   message.Value,
                                                   message.PaymentStatus);
            await _mediator.Send(command);
        }
    }
}
