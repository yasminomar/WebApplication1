using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ProductRepository:GenericRepository<Products> , IProductRepository
    {
        private readonly OnlineStoreContext _context;
        public ProductRepository(OnlineStoreContext context):base(context)
        {
            _context = context;
        }

        public List<Products> GetGroup(List<Guid> ids)
        {
            return _context.Products.Where(i => ids.Contains(i.Id)).ToList();
        }
        public void DeleteProductsByCategoryId(Guid categoryId)
        {
            var products = _context.Products.Where(i => i.CategoryId==categoryId).ToList();
            foreach (var product in products)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

        }

       public List<Products> GetAllProductsSorted(ProductParameters productParameters)
        {
            return _context
                   .Products
                   .OrderBy(p => p.CategoryId)
                   .ThenBy(p => p.EnglishName)
                   .Where(p => p.Quantity > 0)
                   .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                   .Take(productParameters.PageSize)
                   .ToList();
        }


        public List<Products> getFilteredProducts(ProductParameters productParameters,string productName)
        {
            return _context
                   .Products
                   .OrderBy(p => p.CategoryId)
                   .ThenBy(p => p.EnglishName)
                   .Where(p => p.Quantity > 0 && p.EnglishName.Contains(productName))
                   .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                   .Take(productParameters.PageSize)
                   .ToList();
        }
        
        public int GetNumOfProducts()
        {
            return _context.Products.Count();

        }
        public List<Products> GetProductsByIds(string productsIds)
        {
            return _context.Products.Where(p => productsIds.Contains(p.Id.ToString())).ToList();
        }
        public void UpdateProductQuantity(Guid id,int quantity)
        {
            var Product = _context.Products.FirstOrDefault(i => i.Id == id);
            Product.Quantity = quantity;
        }


    }
}
