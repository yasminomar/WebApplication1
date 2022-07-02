using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Products
    {
        public Guid Id { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Categories")]
        public Guid CategoryId { get; set; }
        public virtual Categories? Category { get; set; }

        [ForeignKey("Vendor")]
        public Guid VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public string Image { get; set; }
    }
}
