using System.Collections.Generic;
using WebApplication1.DTO;

namespace WebApplication1.DTO_s.Products
{
    public class CategoryPaginationReadDto
    {
        public int TotalCount { get; set; }
        public List<ProductReadDto> Products { get; set; }
    }
}
