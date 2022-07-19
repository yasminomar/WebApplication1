using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<ProductInCart> Products { get; set; }
    }
}
