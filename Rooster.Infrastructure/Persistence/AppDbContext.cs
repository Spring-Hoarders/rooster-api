using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rooster.Domain.Account;
using Rooster.Domain.Apartment;
using Rooster.Domain.Client;
using Rooster.Domain.Payment;
using Contract = Rooster.Domain.Contract.Contract;

namespace Rooster.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Domain.Building.Building> Buildings { get; set; }
    public DbSet<Domain.Building.Floor> Floor { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Contract> Contract { get; set; }
    public DbSet<Apartment> Apartment { get; set; }
    public DbSet<ApartmentType> ApartmentTypes { get; set; }



    public AppDbContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        modelBuilder.HasDbFunction(
                typeof(DbCustomFunctions).GetMethod(nameof(DbCustomFunctions.DateTrunc),
                    new[] { typeof(string), typeof(DateTime), })!)
            .HasName("date_trunc");

        modelBuilder.HasDbFunction(
                typeof(DbCustomFunctions).GetMethod(nameof(DbCustomFunctions.DateTrunc),
                    new[] { typeof(string), typeof(DateTimeOffset), })!)
            .HasName("date_trunc");
        
        modelBuilder.HasDbFunction(
                typeof(DbCustomFunctions).GetMethod(nameof(DbCustomFunctions.TimeZone),
                    new[] { typeof(string), typeof(DateTimeOffset), })!)
            .HasName("timezone");
    }
}

public static class DbCustomFunctions
{
    public static DateTime DateTrunc(string type, DateTime date)
        => throw new NotSupportedException();

    public static DateTime DateTrunc(string type, DateTimeOffset date)
        => throw new NotSupportedException();

    public static DateTime TimeZone(string timezone, DateTimeOffset date)
        => throw new NotSupportedException();
}

public class DateTruncTypes
{
    public const string Day = "day";
    public const string Hour = "hour";
    public const string Month = "month";

    public const string Year = "year";
    //More fields types be found https://sqlserverguides.com/postgresql-date_trunc-function/
}