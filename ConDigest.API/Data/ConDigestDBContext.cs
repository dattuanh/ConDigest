using ConDigest.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ConDigest.API.Data
{
    public class ConDigestDBContext : DbContext
    {
        public ConDigestDBContext(DbContextOptions<ConDigestDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductItemImage> ProductItemImages { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
