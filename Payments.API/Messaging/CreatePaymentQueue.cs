using MassTransit;
using Payments.Domain.Interfaces.Messaging;
using Payments.Domain.Request;

namespace Payments.API.Messaging
{
    public class CreatePaymentQueue : BaseQueue, ICreatePaymentQueue
    {
        protected const string _connectionQueue = "queue:payments:createpayment";
        public CreatePaymentQueue(IBus bus) : base(bus) { }

        public async Task<bool> SendToQueue(CreatePaymentMessage message)
        {
            try
            {
                var endpoint = await _bus.GetSendEndpoint(new Uri(_connectionQueue));
                await endpoint.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
