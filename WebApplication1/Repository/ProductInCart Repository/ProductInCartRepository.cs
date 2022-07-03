using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ProductInCartRepository : GenericRepository<ProductInCart>, IProductInCartRepository
    {
        private readonly OnlineStoreContext _context;
        public ProductInCartRepository(OnlineStoreContext context) : base(context)
        {
            _context = context;
        }
        //public List<ProductInCart> GetGroup(List<Guid> ids)
        //{
        //    return _context.ProductInCart.Where(i => ids.Contains(i.Id)).ToList();
        //}
        public void DeleteProductsInCartByProductId(Guid productId)
        {
            var productsInCart = _context.ProductInCart.Where(i => i.ProductId == productId).ToList();
            foreach (var productInCart in productsInCart)
            {
                _context.ProductInCart.Remove(productInCart);
            }

        }
        public void DeleteProductsInCartByCartId(Guid cartId)
        {
            var productsInCart = _context.ProductInCart.Where(i => i.CartId == cartId).ToList();
            foreach (var productInCart in productsInCart)
            {
                _context.ProductInCart.Remove(productInCart);
            }

        }

        public List<ProductInCart> GetProductsInCartByCartId(Guid cartId)
        {
            return _context.ProductInCart.Where(i => i.CartId == cartId).ToList();

        }
        public Guid GetProductInCartIdByCartIdAndProductId(Guid cartId,Guid productId)
        {
            var productInCart = _context.ProductInCart.FirstOrDefault(i => i.CartId == cartId && i.ProductId == productId);
            return productInCart.Id;

        }
        //public void DeleteProductInCartIdByCartIdAndProductId(Guid cartId, Guid productId)
        //{
        //    var productInCart = _context.ProductInCart.FirstOrDefault(i => i.CartId == cartId && i.ProductId == productId);
        //    _context.ProductInCart.Remove(productInCart);
        //}


    }
}
