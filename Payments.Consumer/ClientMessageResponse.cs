namespace Payments.Consumer
{
    public class ClientMessageResponse
    {
        public long Id { get; set; }
        public string CpfCnpj { get; set; }
        public string Name { get; set; }
        public string ContractNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal GrossIncome { get; set; }
    }
}
