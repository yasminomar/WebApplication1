using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("CartId")]
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public string ShipmentAddress { get; set; }
        public string PaymentMethod { get; set; }
 


    }
}
