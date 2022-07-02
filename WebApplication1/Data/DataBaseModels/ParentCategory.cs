using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ParentCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<Categories> Categories { get; set; }

    }
}
