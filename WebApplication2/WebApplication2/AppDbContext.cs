using WebApplication2.Model;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2;



public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Model.Program> Programs { get; set; }
    public DbSet<WashingMachine> WashingMachines { get; set; }
    public DbSet<AvailableProgram> AvailablePrograms { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistories { get; set; }


    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WashingMachine>()
            .HasIndex(w => w.SerialNumber)
            .IsUnique();

        modelBuilder.Entity<PurchaseHistory>()
            .HasKey(ph => new { ph.AvailableProgramId, ph.CustomerId });
    
    
        base.OnModelCreating(modelBuilder);

    
    modelBuilder.Entity<Customer>().HasData(
        new Customer
        {
            CustomerId = 1,
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = null
        }
    );

    
    modelBuilder.Entity<WashingMachine>().HasData(
        new WashingMachine
        {
            WashingMachineId = 1,
            SerialNumber = "WM2012/S431/12",
            MaxWeight = 32.23m
        },
        new WashingMachine
        {
            WashingMachineId = 2,
            SerialNumber = "WM2014/S491/28",
            MaxWeight = 52.23m
        }
    );

    
    
    
    modelBuilder.Entity<Model.Program>().HasData(
        new Model.Program
        {
            ProgramId = 1,
            Name = "Quick Wash",
            DurationMinutes = 69,
            TemperatureCelsius = 40
        },
        new Model.Program
        {
            ProgramId = 2,
            Name = "Cotton Cycle",
            DurationMinutes = 143,
            TemperatureCelsius = 60
        }
    );

    
    modelBuilder.Entity<AvailableProgram>().HasData(
        new AvailableProgram
        {
            AvailableProgramId = 1,
            WashingMachineId = 1,
            ProgramId = 1,
            Price = 33.4m
        },
        new AvailableProgram
        {
            AvailableProgramId = 2,
            WashingMachineId = 2,
            ProgramId = 2,
            Price = 48.7m
        }
    );

    
    modelBuilder.Entity<PurchaseHistory>().HasData(
        new PurchaseHistory
        {
            AvailableProgramId = 1,
            CustomerId = 1,
            PurchaseDate = new DateTime(2025, 6, 3, 9, 0, 0),
            Rating = 4
        },
        new PurchaseHistory
        {
            AvailableProgramId = 2,
            CustomerId = 1,
            PurchaseDate = new DateTime(2025, 6, 4, 9, 0, 0),
            Rating = null
        }
    );
}
}
