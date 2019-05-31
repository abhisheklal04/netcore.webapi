using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repository
{
    public partial class CustomerContext : DbContext
    {
        private DbContextOptions options;

        public CustomerContext()
        {
        }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public CustomerContext(DbContextOptions options)
        {
            this.options = options;
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=10.10.10.42;Database=RoadhouseExtranet;Trusted_Connection=True;User ID=sa;Password=rhi3978;");
                optionsBuilder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date");

                entity.Property(e => e.IsArchived).HasColumnType("bit(1)");
            });

        }
    }
}
