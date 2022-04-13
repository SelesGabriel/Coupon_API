using Coupon_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupon_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Coupon>? Coupons { get; set; }
        public DbSet<FavoriteItems>? FavoriteItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(u => u.IdUser);
            builder.Entity<User>().Property(u => u.Name).HasMaxLength(20);
            builder.Entity<User>().Property(u => u.LastName).HasMaxLength(20);
            builder.Entity<User>().Property(u => u.Email).HasMaxLength(30);

            builder.Entity<FavoriteItems>().Property(u => u.IdItem).HasMaxLength(20);
            builder.Entity<FavoriteItems>().Property(u => u.IdUser).HasMaxLength(5);
            builder.Entity<FavoriteItems>().Property(u => u.Amount).HasPrecision(14, 2);
            builder.Entity<FavoriteItems>().HasKey(f => f.Id);

            builder.Entity<Coupon>().HasKey(u => u.Id);
            builder.Entity<Coupon>().Property(u => u.Amount).HasPrecision(14, 2);

            builder.Entity<FavoriteItems>().HasOne<User>(c => c.User).WithMany(f => f.Favorites).HasForeignKey(u => u.IdUser);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite("DataSource=app.db");
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseMySql("Server=uyu7j8yohcwo35j3.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;DataBase=qjmyeifpkiy1rj4d;Uid=h90qchpk35uy8xe4;Pwd=pii53w106qhyh35w",
        //    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
    }
}
