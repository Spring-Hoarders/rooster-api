using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooster.Domain.Account;
using Rooster.Domain.Payment;

namespace Rooster.Infrastructure.Persistence.Common.Configuration
{
    public class PaymentConfiguration : BaseConfiguration, IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payment)).HasKey(x => x.Id);

            builder.HasOne(p => p.Client)
                .WithMany() 
                .IsRequired()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Contract)
                .WithMany() 
                .IsRequired()
                .HasForeignKey(p => p.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.FileUrl).IsRequired().HasMaxLength(StringMaxLength);
            builder.Property(p => p.Amount).IsRequired().HasMaxLength(IntMaxLenth);
            builder.Property(p => p.CreatedAt).IsRequired().HasMaxLength(StringMaxLength);
        }
    }

}
