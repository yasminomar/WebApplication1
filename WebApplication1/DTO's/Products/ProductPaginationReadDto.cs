using System.Collections.Generic;

namespace WebApplication1.DTO_s.Products
{
    public class ProductPaginationReadDto
    {
        public int TotalCount { get; set; }
        public List<ProductGroupingOutputReadDto> Products { get; set; }
    }
}
