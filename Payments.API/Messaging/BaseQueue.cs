using MassTransit;
using Payments.Domain.Interfaces.Messaging;

namespace Payments.API.Messaging
{
    public class BaseQueue : IBaseQueue
    {
        protected readonly IBus _bus;
        public BaseQueue(IBus bus)
        {
            _bus = bus;
        }
    }
}
