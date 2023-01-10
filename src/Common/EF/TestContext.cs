using Microsoft.EntityFrameworkCore;
using Common.Entities;
using Common.Interfaces;

namespace Common.EF;
public class TestContext : DbContext, IDbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Factory> Factories { get; set; }
    public DbSet<FactoryToCustomer> FactoriesToCustomer { get; set; }

    public DbContext Instance => this;

    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>().ToTable("Groups").HasKey(x => x.groupCode);
        modelBuilder.Entity<Group>().Property(x => x.groupCode).HasColumnName("groupKod");
        modelBuilder.Entity<Customer>().ToTable("Customers").HasKey(x => x.customerId);
        // modelBuilder.Entity<Customer>().Property(x => x.name).HasColumnName("name");
        // modelBuilder.Entity<Customer>().Property(x => x.phone).HasColumnName("phone");
        modelBuilder.Entity<Factory>().ToTable("Factories").HasKey(x => x.factoryCode);
        // modelBuilder.Entity<Factory>().Property(x => x.factoryCode).HasColumnName("factoryCode");
        // modelBuilder.Entity<Factory>().Property(x => x.factoryName).HasColumnName("factoryName");
        modelBuilder.Entity<Factory>().HasOne(x => x.Group).WithMany(x => x.Factories).HasForeignKey(x => x.groupCode);
        modelBuilder.Entity<FactoryToCustomer>().ToTable("FactoriesToCustomer").HasKey(x => new { x.factoryCode, x.groupCode, x.customerId });
        modelBuilder.Entity<FactoryToCustomer>().HasOne(x => x.Group).WithMany(x => x.FactoriesToCustomer).HasForeignKey(x => x.groupCode);
        modelBuilder.Entity<FactoryToCustomer>().HasOne(x => x.Factory).WithMany(x => x.FactoriesToCustomer).HasForeignKey(x => x.factoryCode);
        modelBuilder.Entity<FactoryToCustomer>().HasOne(x => x.Customer).WithMany(x => x.FactoriesToCustomer).HasForeignKey(x => x.customerId);
    }
}