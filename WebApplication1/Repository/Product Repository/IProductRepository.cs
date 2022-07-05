using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data.DataBaseModels;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IProductRepository:IGenericRepository<Products>
    {
        List<Products> GetGroup(List<Guid> ids);
        List<ProductGroupingOutput> GetAllProductsSorted(ProductParameters productParameters);
        List<ProductGroupingOutput> getFilteredProducts(ProductParameters productParameters, string productName);

        void DeleteProductsByCategoryId(Guid categoryId);
        int GetNumOfProducts();
        List<Products> GetProductsByIds(string productsIds);
        void UpdateProductQuantity(Guid id, int quantity);







    }
}