using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;

namespace Payments.Infra.Data.Configs
{

    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.ContractNumber).IsRequired();
            builder.Property(x => x.Quota).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired();
            builder.ToTable("Payments");
        }
    }
}
