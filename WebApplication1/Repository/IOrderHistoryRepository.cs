using System.Collections.Generic;
using WebApplication1.Data.DataBaseModels;

namespace WebApplication1.Repository
{
    public interface IOrderHistoryRepository
    {
        void AddOrderHistory(OrderHistory orderHistory);
        List<OrderHistory> GetOrders(string userId);
        void SaveChanges();

    }
}