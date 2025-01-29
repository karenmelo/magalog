using Magalog.Domain.Entitites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Magalog.Data.Mappings;

internal class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Value)
               .HasColumnType("decimal(10,2)");

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => new { oi.Order_id, oi.Date });

        builder.ToTable("OrderItems");
    }
}
