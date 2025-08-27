using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TF_IOT2025_DemoJwt_API.Entities.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Username).IsRequired();

            builder.Property(u => u.Password).IsRequired();

            builder.Property(u => u.Role).IsRequired().HasConversion<string>();
        }
    }
}
