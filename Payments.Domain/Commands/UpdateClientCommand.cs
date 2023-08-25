using MediatR;

namespace Payments.Domain.Commands
{
    public class UpdateClientCommand : IRequest<GenericCommand>
    {
        public UpdateClientCommand(long id, string cpfCnpj, string name, string contractNumber, string city, string state, decimal grossIncome)
        {
            Id = id;
            CpfCnpj = cpfCnpj;
            Name = name;
            ContractNumber = contractNumber;
            City = city;
            State = state;
            GrossIncome = grossIncome;
        }
        public long Id { get; set; }
        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal GrossIncome { get; set; }
    }
}
