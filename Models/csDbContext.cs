using Microsoft.EntityFrameworkCore;

namespace dotnet_cs_api.Models
{
    public class csDbContext : DbContext
    {
        public csDbContext(DbContextOptions<csDbContext> options): base(options)
        {
            
        }
        public DbSet<TblProduct> TblProducts { get; set; }
        public DbSet<TblOrder>  TblOrders { get; set; }
        public DbSet<TblOrderItem> TblOrderItems { get; set; }
        public DbSet<TblCustomer> TblCustomers { get; set; }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblOrder>()
                    .HasMany(c => c.OrderItems)
                    .WithOne(e => e.Order);
    }
    }
}