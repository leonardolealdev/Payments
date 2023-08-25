using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;
using Payments.Domain.Interfaces;
using Payments.Domain.Responses;

namespace Payments.Infra.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(PaymentDbContext dbContext) : base(dbContext) { }

        public async Task<long> UpdateClient(Client client)
        {
            var entityEntry = _dbContext.Entry(client);
            entityEntry.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckExistingCpfCnpj(string cpfCnpj, long id)
        {

            if (id == 0)
                return await _dbContext.Clients.AnyAsync(x => x.CpfCnpj.Equals(cpfCnpj));
            
            return await _dbContext.Clients.AnyAsync(x => x.CpfCnpj.Equals(cpfCnpj) && x.Id != id);
        }

        public async Task<bool> CheckExistingContractNumber(string contractNumber, long id)
        {

            if (id == 0)
                return await _dbContext.Clients.AnyAsync(x => x.ContractNumber.Equals(contractNumber));

            return await _dbContext.Clients.AnyAsync(x => x.ContractNumber.Equals(contractNumber) && x.Id != id);
        }

        public async Task<Client> GetById(long id)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Client> GetByContractNumber(string contractNumber)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(x => x.ContractNumber.Equals(contractNumber));
        }
    }
}
