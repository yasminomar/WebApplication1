using System;

namespace WebApplication1.DTO
{
    public record OrderWriteDto
    {
        public Guid CartId { get; init; }
        public string ShipmentAddress { get; set; }
        public string PaymentMethod { get; set; }


    }
}
