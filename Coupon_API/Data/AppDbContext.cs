using Coupon_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupon_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Coupon>? Coupons { get; set; }
        public DbSet<Item>? Items { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<Coupon>().HasKey(x => x.Id);
            builder.Entity<Item>().HasKey(x => x.Id);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite("DataSource=app.db");
    }
}
