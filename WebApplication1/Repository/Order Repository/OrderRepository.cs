using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class OrderRepository :GenericRepository<Order> , IOrderRepository
    {
        private readonly OnlineStoreContext _context;
        public OrderRepository(OnlineStoreContext context) : base(context)
        {
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


    }
}
