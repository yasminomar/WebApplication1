using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.DataBaseModels;

namespace WebApplication1.Models
{
    public class OnlineStoreContext:IdentityDbContext<ApplicationUser>
    {
        public OnlineStoreContext() 
        {

        }
        public OnlineStoreContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Products> Products { get; set; }
        public DbSet<ParentCategory> ParentCategory { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<ProductInCart> ProductInCart { get; set; }
       // public DbSet<ProductParameters> ProductParameters { get; set; }


    }
}
