using System;
using System.Collections.Generic;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ICategoryRepository:IGenericRepository<Categories>
    {
        void DeleteCategoriesByParentCategoryId(Guid parentCategoryId);
        List<Categories> GetAllCategoriesSorted(CategoryParameters categoryParameters);
        int GetNumOfCategories();


    }
}