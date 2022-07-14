using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class OrderHistoryRepository : IOrderHistoryRepository
    {
        private readonly OnlineStoreContext _context;
        public OrderHistoryRepository(OnlineStoreContext context)
        {
            _context = context;
        }
        public List<OrderHistory> GetOrders(string userId)
        {
            return _context.OrderHistory.Where(i => i.UserId == userId).ToList();

        }

        public void AddOrderHistory(OrderHistory orderHistory)
        {
            _context.OrderHistory.Add(orderHistory);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
