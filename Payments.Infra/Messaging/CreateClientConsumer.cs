using MassTransit;
using MediatR;
using Payments.Domain.Commands;
using Payments.Domain.Request;

namespace Payments.Infra.Messaging
{
    public class CreateClientConsumer : IConsumer<CreateClientMessage>
    {
        private readonly IMediator _mediator;
        public CreateClientConsumer(IMediator mediator) 
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<CreateClientMessage> context)
        {
            var message = context.Message;
            var command = new CreateClientCommand(message.CpfCnpj, message.Name, message.ContractNumber, message.City, message.State, message.GrossIncome);
            await _mediator.Send(command);
        }
    }
}
