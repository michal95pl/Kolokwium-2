using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EfConfigurations;

public class SubscriptionEfConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");

        builder.HasKey(s => s.IdSubscription);
        builder.Property(s => s.IdSubscription).ValueGeneratedOnAdd();

        builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
        builder.Property(s => s.RenewalPeriod).IsRequired();
        builder.Property(s => s.EndTime).IsRequired();
        builder.Property(s => s.Price).IsRequired();
    }
}