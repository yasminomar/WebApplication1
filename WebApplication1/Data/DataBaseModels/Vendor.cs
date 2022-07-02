using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Mobile { get; set; }
        public virtual List<Products> Products { get; set; }

    }
}
