using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
     public interface IProductInCartRepository : IGenericRepository<ProductInCart>
    {
        //List<ProductInCart> GetGroup(List<Guid> ids);
        void DeleteProductsInCartByProductId(Guid productId);
        void DeleteProductsInCartByCartId(Guid cartId);
        List<ProductInCart> GetProductsInCartByCartId(Guid cartId);





    }
}