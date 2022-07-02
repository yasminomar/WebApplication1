using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Data.DataBaseModels
{
    public class ProductGroupingOutput
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Products> Products { get; set; }

    }
}
