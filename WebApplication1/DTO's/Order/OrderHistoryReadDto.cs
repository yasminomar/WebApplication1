using System;
using System.Collections.Generic;
using WebApplication1.DTO;

namespace WebApplication1.DTO_s.Order
{
    public class OrderHistoryReadDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int TotalPrice { get; set; }
        public string ProductsIds { get; set; }
        public List<ProductReadDto> Products { get; set; }
        public string ShippmentAddress { get; set; }
        public string PaymentMethod { get; set; }

    }
}
