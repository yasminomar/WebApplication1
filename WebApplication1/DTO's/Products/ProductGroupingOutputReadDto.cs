using System;
using System.Collections.Generic;
using WebApplication1.DTO;

namespace WebApplication1.DTO_s.Products
{
    public class ProductGroupingOutputReadDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductReadDto> Products { get; set; }
    }
}
