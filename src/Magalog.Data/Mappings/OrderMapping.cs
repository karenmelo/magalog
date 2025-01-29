using Magalog.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magalog.Data.Mappings;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => new { o.Order_id, o.Date });

        builder.Property(o => o.Order_id)
               .ValueGeneratedNever();

        builder.Property(o => o.Total)
               .HasColumnType("decimal(10,2)");

        builder.Property(o => o.Date)
               .HasColumnType("date");
        
        builder.HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.User_id)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Order");

    }
}