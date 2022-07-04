using System;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Order GetOrderByCartId(Guid cartId);
        void DeleteOrderByCartId(Guid cartId);



    }
}