using System;
using System.Linq;
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



    }
}
