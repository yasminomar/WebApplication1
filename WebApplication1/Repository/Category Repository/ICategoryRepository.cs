using System;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICategoryRepository:IGenericRepository<Categories>
    {
        void DeleteCategoriesByParentCategoryId(Guid parentCategoryId);

    }
}