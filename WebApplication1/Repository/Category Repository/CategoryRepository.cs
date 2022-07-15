using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CategoryRepository:GenericRepository<Categories> , ICategoryRepository
    {
        private readonly OnlineStoreContext _context;
        private readonly IProductRepository productRepo;

        public CategoryRepository(IProductRepository productRepo, OnlineStoreContext context) : base(context)
        {
            this.productRepo = productRepo;
            _context = context;
        }
        public void DeleteCategoriesByParentCategoryId(Guid parentCategoryId)
        {
            var categories = _context.Categories.Where(i => i.ParentCategoryId==parentCategoryId).ToList();
            foreach (var category in categories)
            {
                productRepo.DeleteProductsByCategoryId(category.Id);
                _context.Categories.Remove(category);
            }

        }



        public List<Categories> GetAllCategoriesSorted(CategoryParameters categoryParameters)
        {
            return _context
                   .Categories
                   .OrderBy(c => c.Name)
                   .Skip((categoryParameters.PageNumber - 1) * categoryParameters.PageSize)
                   .Take(categoryParameters.PageSize)
                   .ToList();
        }

        public int GetNumOfCategories()
        {
            return _context.Categories.Count();

        }

    }
}
