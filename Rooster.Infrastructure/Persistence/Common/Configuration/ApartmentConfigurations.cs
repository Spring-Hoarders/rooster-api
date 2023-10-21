using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooster.Domain.Apartment;


namespace Rooster.Infrastructure.Persistence.Common.Configuration;

public class ApartmentConfiguration : BaseConfiguration, IEntityTypeConfiguration<Apartment>
{
    private const int FloorMaxNumber = 999;
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable(nameof(Apartment)).HasKey(x => x.Id);
        
        builder.Property(x => x.Name).HasMaxLength(FloorMaxNumber);

        builder.HasOne(f => f.Floor)
            .WithMany()
            .IsRequired()
            .HasForeignKey(f => f.FloorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.ApartmentType)
            .WithMany()
            .IsRequired()
            .HasForeignKey(f => f.ApartmentTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class ApartmentTypeConFiguration : BaseConfiguration, IEntityTypeConfiguration<ApartmentType>
{
    private const int ApartmentCapacityMaxNumber = 4;
    public void Configure(EntityTypeBuilder<ApartmentType> builder)
    {
        builder.ToTable(nameof(ApartmentType)).HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(StringMaxLength);
        builder.Property(x => x.Capacity).HasMaxLength(ApartmentCapacityMaxNumber);
        builder.Property(x => x.Price).IsRequired();
    }
}
