using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        void Add(Cart cart);
        Cart CartByUserId(string userid);



    }
}