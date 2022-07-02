﻿using System;
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
                   .GroupBy(i => new { i.Category.Name, i.Category.Id })
                   .Select(g => new ProductGroupingOutput
                   {
                       CategoryId=g.Key.Id,
                       CategoryName=g.Key.Name,
                       Products=g.ToList()
                   })
                   .ToList();
           // .AsEnumerable().OrderBy(p => p.EnglishName)


           // return _context.GroupBy(i => new { i.Category.Name })

        }
        //public 
    }
}