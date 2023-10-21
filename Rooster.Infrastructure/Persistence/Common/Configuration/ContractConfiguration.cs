using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Contracts;
using Rooster.Domain.Contract;

namespace Rooster.Infrastructure.Persistence.Common.Configuration
{
    public class ContractConfiguration : BaseConfiguration, IEntityTypeConfiguration<Domain.Contract.Contract>
    {
        public void Configure(EntityTypeBuilder<Domain.Contract.Contract> builder)
        {
            builder.ToTable(nameof(Domain.Contract.Contract)).HasKey(x => x.Id);

            builder.HasOne(c => c.Apartment)
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Client)
                .WithMany()
                .IsRequired()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.AccommodationDate)
                .HasColumnType("timestamp with time zone");

            builder.Property(c => c.EvictionDate)
                .HasColumnType("timestamp with time zone");

            builder.Property(c => c.StartDate)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(c => c.EndDate)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(c => c.Guarantee)
                .IsRequired();
        }
    }
}
