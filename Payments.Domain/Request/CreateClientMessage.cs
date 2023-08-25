namespace Payments.Domain.Request
{
    public class CreateClientMessage 
    {
        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal GrossIncome { get; set; }
    }
}
