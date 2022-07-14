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
            }

        }

       public List<ProductGroupingOutput> GetAllProductsSorted(ProductParameters productParameters)
        {
            return _context
                   .Products
                   .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                   .Take(productParameters.PageSize)
                   .AsEnumerable()
                   .GroupBy(i => new { i.Category.Name, i.Category.Id })
                   .Select(g => new ProductGroupingOutput
                   {
                       CategoryId=g.Key.Id,
                       CategoryName=g.Key.Name,
                       Products=g.OrderBy(p => p.EnglishName).Where(p=>p.Quantity>0).ToList()
                   })
                   .ToList();
        }


        public List<ProductGroupingOutput> getFilteredProducts(ProductParameters productParameters,string productName)
        {
            return _context
                   .Products
                   .Where(p => p.Quantity > 0 && p.EnglishName.Contains(productName,StringComparison.InvariantCultureIgnoreCase)).ToList()
                   .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                   .Take(productParameters.PageSize)
                   .AsEnumerable()
                   .GroupBy(i => new { i.Category.Name, i.Category.Id })
                   .Select(g => new ProductGroupingOutput
                   {
                       CategoryId = g.Key.Id,
                       CategoryName = g.Key.Name,
                       Products = g.OrderBy(p => p.EnglishName).ToList()
                   })
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
