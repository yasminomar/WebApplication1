using System.Collections.Generic;
using WebApplication1.DTO;

namespace WebApplication1.DTO_s.Categories
{
    public class CategoryPaginationReadDto
    {
        public int TotalCount { get; set; }
        public List<CategoryReadDto> Categories { get; set; }
    }
}
