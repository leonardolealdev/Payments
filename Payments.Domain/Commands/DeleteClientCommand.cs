using MediatR;
using System.Xml.Linq;

namespace Payments.Domain.Commands
{
    public class DeleteClientCommand : IRequest<GenericCommand>
    {
        public DeleteClientCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
