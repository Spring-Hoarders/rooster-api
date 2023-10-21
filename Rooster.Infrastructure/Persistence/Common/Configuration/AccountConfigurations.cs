using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rooster.Domain.Account;

namespace Rooster.Infrastructure.Persistence.Common.Configuration;

public class UserConfiguration : BaseConfiguration, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User)).HasKey(c => c.Id);
        builder.Property(x => x.Username).HasMaxLength(StringMaxLength);
        builder.Property(x => x.Password).HasMaxLength(StringMaxLength);
        builder.Property(x => x.Email).HasMaxLength(StringMaxLength);
    }
}

public class UserRoleConfiguration : BaseConfiguration, IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(nameof(UserRole)).HasKey(f => new { f.UserId, f.RoleId });

        builder.HasOne(f => f.User)
            .WithMany()
            .IsRequired()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<User>()
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<User>()
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(c => c.LastModifiedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}