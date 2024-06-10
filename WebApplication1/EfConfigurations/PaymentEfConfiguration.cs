using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class PaymentEfConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.IdPayment);
        builder.Property(p => p.IdPayment).ValueGeneratedOnAdd();

        builder.Property(p => p.Date).IsRequired();
        
        builder.HasOne<Client>(p => p.Client)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.IdClient);
        
        builder.HasOne<Subscription>(p => p.Subscription)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.IdSubscription);
    }
}