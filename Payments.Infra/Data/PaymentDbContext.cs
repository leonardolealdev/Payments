using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;
using System.Reflection;

namespace Payments.Infra.Data
{
    public class PaymentDbContext : DbContext
    {
        #region DBSets
        public DbSet<Client> Clients { get; set; }
        public DbSet<Payment> Payments { get; set; }
        #endregion

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
