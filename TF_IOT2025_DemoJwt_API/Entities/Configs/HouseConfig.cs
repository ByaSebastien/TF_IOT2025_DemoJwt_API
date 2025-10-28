using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TF_IOT2025_DemoJwt_API.Entities.Configs
{
    public class HouseConfig : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).ValueGeneratedOnAdd();

            builder.Property(h => h.Name).IsRequired();

            builder.HasMany(h => h.Users)
                   .WithMany(u => u.Houses);
        }
    }
}
