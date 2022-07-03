using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data.DataBaseModels
{
    [Table("OrderHistory")]
    public class OrderHistory
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int TotalPrice { get; set; }
        public string ProductsIds { get; set; }
        public string ShippmentAddress { get; set; }
        public string PaymentMethod { get; set; }



    }
}
