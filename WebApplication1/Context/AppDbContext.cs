using Microsoft.EntityFrameworkCore;
using WebApplication1.EfConfigurations;
using WebApplication1.Models;

namespace WebApplication1.Context;

public class AppDbContext : DbContext
{

    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDbContext(DbContextOptions options, IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientEfConfiguration());
        modelBuilder.ApplyConfiguration(new DiscountEfConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentEfConfiguration());
        modelBuilder.ApplyConfiguration(new SaleEfConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionEfConfiguration());
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
}