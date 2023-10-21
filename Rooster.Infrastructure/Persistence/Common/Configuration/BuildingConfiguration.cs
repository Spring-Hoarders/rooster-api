using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooster.Domain.Account;
using Rooster.Domain.Building;

namespace Rooster.Infrastructure.Persistence.Common.Configuration
{
    public class BuildingConfiguration : BaseConfiguration, IEntityTypeConfiguration<Building>
    {
        
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable(nameof(Building)).HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(StringMaxLength);
        }


        public class FloorConfiguration : IEntityTypeConfiguration<Floor>
        {
            public void Configure(EntityTypeBuilder<Floor> builder)
            {
                builder.ToTable(nameof(Floor)).HasKey(x => x.Id);
                builder.Property(x => x.Number).HasMaxLength(IntMaxLenth);

                builder.HasOne(f => f.Building)
                    .WithMany()
                    .IsRequired()
                    .HasForeignKey(f => f.BuildingId)
                    .OnDelete(DeleteBehavior.Restrict);

            }
        }

    }
}
