using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApi.Data
{
    public class EcomDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public EcomDbContext()
        {
        }

        public EcomDbContext(DbContextOptions<EcomDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                        .Property(x => x.Id)
                        .ValueGeneratedOnAdd(); // configures Id as an Identity Column

            modelBuilder.Entity<User>()
                .Property(x => x.UserName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnType("text");

            modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnType("text");
            modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnType("text");
        }

    }
}
