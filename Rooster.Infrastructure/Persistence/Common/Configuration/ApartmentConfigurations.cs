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

        builder.HasOne(x => x.RoomType)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class RoomTypeConFiguration : BaseConfiguration, IEntityTypeConfiguration<RoomType>
{
    private const int RoomMaxNumber = 3;
    public void Configure(EntityTypeBuilder<RoomType> builder)
    {
        builder.ToTable(nameof(RoomType)).HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(StringMaxLength);
        builder.Property(x => x.Capacity).HasMaxLength(RoomMaxNumber);
    }
}
