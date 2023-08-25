using Microsoft.EntityFrameworkCore;
using Payments.Domain.Interfaces;

namespace Payments.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PaymentDbContext _dbContext;
        protected readonly DbSet<T> _currentSet;

        public Repository(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
            _currentSet = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _currentSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var entityEntry = _dbContext.Entry(entity);
            entityEntry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _currentSet.ToListAsync();

        public async Task Delete(T entity)
        {
            _currentSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
