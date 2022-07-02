using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Categories
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("ParentCategory")]
        public Guid ParentCategoryId { get; set; }
        public virtual ParentCategory ParentCategory { get; set; }
        public virtual List<Products> Products { get; set; }


    }
}
