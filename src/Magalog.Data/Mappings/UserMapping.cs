using Magalog.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magalog.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.User_Id);

        builder.Property(u => u.User_Id)
               .ValueGeneratedNever();

        builder.Property(u => u.Name)
               .IsRequired()
               .HasColumnType("varchar(250)");


        builder.ToTable("User");
    }
}
