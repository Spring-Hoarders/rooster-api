using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooster.Domain.Account;
using Rooster.Domain.Client;
using Rooster.Domain.Payment;

namespace Rooster.Infrastructure.Persistence.Common.Configuration

{
    public class ClientConfiguration : BaseConfiguration, IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(nameof(Client)).HasKey(x => x.Id);
            builder.HasOne(c => c.Apartment)
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(StringMaxLength);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(StringMaxLength);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(StringMaxLength); 
        }
    }
}
