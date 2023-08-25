using MassTransit;
using Payments.Domain.Interfaces.Messaging;
using Payments.Domain.Request;

namespace Payments.API.Messaging
{
    public class CreateClientQueue : BaseQueue, ICreateClientQueue
    {
        protected const string _connectionQueue = "queue:payments:createclient";
        public CreateClientQueue(IBus bus) : base(bus) { }

        public async Task<bool> SendToQueue(CreateClientMessage message)
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
