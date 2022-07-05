using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;




namespace WebApplication1.Repository
{
    public class OrderRepository :GenericRepository<Order> , IOrderRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly OnlineStoreContext _context;
        public OrderRepository(UserManager<ApplicationUser> userManager, OnlineStoreContext context) : base(context)
        {
            this.userManager = userManager;
            _context = context;
        }
        public Order GetOrderByCartId(Guid cartId)
        {
            return _context.Order.FirstOrDefault(i=>i.CartId == cartId);  
        }

        public void DeleteOrderByCartId(Guid cartId)
        {

          var deletedOrder=  _context.Order.FirstOrDefault(i => i.CartId == cartId);
            _context.Order.Remove(deletedOrder);

        }
        //public  string GetUserId()
        //{
        //    var username = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //    var user =  userManager.FindByNameAsync(username);
        //    return user.Id;
        //}


    }
}
