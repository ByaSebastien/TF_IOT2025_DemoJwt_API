using Microsoft.EntityFrameworkCore;

namespace TF_IOT2025_DemoJwt_API.Entities.Contexts
{
    public class MyDbContext: DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<House> Houses => Set<House>();

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
        }
    }
}
