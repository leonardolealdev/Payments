using Payments.Domain.Commands;

namespace Payments.Domain.Entities
{
    public class Client : EntityBase
    {
        protected Client() { }
        public Client(string cpfCnpj, string name, string contractNumber, string city, string state, decimal grossIncome)
        {
            CpfCnpj = cpfCnpj;
            Name = name;
            ContractNumber = contractNumber;
            City = city;
            State = state;
            GrossIncome = grossIncome;
        }

        public string CpfCnpj { get; private set; }
        public string Name { get; private set; }
        public string ContractNumber { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public decimal GrossIncome { get; private set; }

        public void Update(UpdateClientCommand request)
        {
            CpfCnpj = request.CpfCnpj;
            Name = request.Name;
            ContractNumber = request.ContractNumber;
            City = request.City;
            State = request.State;
            GrossIncome = request.GrossIncome;
        }
    }
}
