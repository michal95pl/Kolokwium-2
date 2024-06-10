using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class DiscountEfConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(d => d.IdDiscount);
        builder.Property(d => d.IdDiscount).ValueGeneratedOnAdd();

        builder.Property(d => d.Value).IsRequired();
        
        builder.HasOne<Subscription>(d => d.Subscription)
            .WithMany(s => s.Discounts)
            .HasForeignKey(d => d.IdSubscription);

        builder.Property(d => d.DateFrom).IsRequired();
        builder.Property(d => d.DateTo).IsRequired();
    }
}