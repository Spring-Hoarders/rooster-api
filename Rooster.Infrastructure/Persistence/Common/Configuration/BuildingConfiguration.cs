using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooster.Domain.Account;
using Rooster.Domain.Building;

namespace Rooster.Infrastructure.Persistence.Common.Configuration
{
    public class BuildingConfiguration : BaseConfiguration, IEntityTypeConfiguration<Domain.Building.Building>
    {
        
        public void Configure(EntityTypeBuilder<Domain.Building.Building> builder)
        {
            builder.ToTable(nameof(Building)).HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(StringMaxLength);
        }


        public class FloorConfiguration : IEntityTypeConfiguration<Floor>
        {
            private const int MaxFloorNumber = 30; 
            public void Configure(EntityTypeBuilder<Floor> builder)
            {
                builder.ToTable(nameof(Floor)).HasKey(x => x.Id);
                builder.Property(x => x.Number).HasMaxLength(MaxFloorNumber);

                builder.HasOne(f => f.Building)
                    .WithMany()
                    .IsRequired()
                    .HasForeignKey(f => f.BuildingId)
                    .OnDelete(DeleteBehavior.Restrict);

            }
        }

    }
}
