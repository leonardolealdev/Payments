using Payments.Domain.Request;

namespace Payments.Domain.Interfaces.Messaging
{
    public interface ICreatePaymentQueue
    {
        Task<bool> SendToQueue(CreatePaymentMessage message);
    }
}
