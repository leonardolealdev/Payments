using Payments.Domain.Request;

namespace Payments.Domain.Interfaces.Messaging
{
    public interface ICreateClientQueue
    {
        Task<bool> SendToQueue(CreateClientMessage message);
    }
}
