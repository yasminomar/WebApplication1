using System;
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

    
    }
}
