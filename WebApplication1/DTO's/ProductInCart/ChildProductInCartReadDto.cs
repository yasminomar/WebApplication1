using System;

namespace WebApplication1.DTO
{
    public record ChildProductInCartReadDto
    {
        public Guid Id { get; init; }
        public ProductReadDto Product { get; init; }
        public int Quantity { get; set; }


    }
}
