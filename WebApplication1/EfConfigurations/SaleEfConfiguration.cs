using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class SaleEfConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.IdSale);
        builder.Property(s => s.IdSale).ValueGeneratedOnAdd();

        builder.HasOne<Subscription>(s => s.Subscription)
            .WithMany(s => s.Sales)
            .HasForeignKey(s => s.IdSubscription);
        
        builder.Property(s => s.CreatedAt).IsRequired();
    }
}