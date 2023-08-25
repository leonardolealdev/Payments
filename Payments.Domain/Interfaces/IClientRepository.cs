using Payments.Domain.Entities;
using Payments.Domain.Responses;

namespace Payments.Domain.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<long> UpdateClient(Client client);
        Task<bool> CheckExistingCpfCnpj(string cpfCnpj, long id);
        Task<bool> CheckExistingContractNumber(string contractNumber, long id);
        Task<Client> GetById(long id);
        Task<Client> GetByContractNumber(string contractNumber);
    }
}
