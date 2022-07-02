using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CartRepository:GenericRepository<Cart> , ICartRepository
    {
        private readonly OnlineStoreContext _context;
        public CartRepository(OnlineStoreContext context) : base(context)
        {
            _context = context;
        }
        public void Add(Cart cart)
        {
            _context.Cart.Add(cart);
            _context.SaveChanges();
        }
        public Cart CartByUserId(string userid)
        {
            return _context.Cart.FirstOrDefault(i => i.ApplicationUser.Id == userid);
        }

        //public Cart Catr(string userId)
        //{
        //    var email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        //    var user = await _userManager.FindByEmailAsync(email); return _context.Cart.Where(i=>i.ApplicationUser.Id==)
        //}

     
    }
}
