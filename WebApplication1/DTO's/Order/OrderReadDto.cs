using System;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class OrderReadDto
    {
        public Guid Id { get; init; }
        public virtual ApplicationUser ApplicationUser { get; init; }
        public ChildCartReadDto Cart { get; init; }
        public string ShipmentAddress { get; set; }
        public string PaymentMethod {get; set;}


    }
}
