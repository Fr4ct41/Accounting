using DataBase.Entity;
using DataBase.Seed;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class IncomeExpenseContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<LoginHistory> LoginHistories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IncomeExpenseDb;Trusted_Connection=True;");    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new RoleSeed(modelBuilder).Seed();
        new UserSeed(modelBuilder).Seed();
    }
}