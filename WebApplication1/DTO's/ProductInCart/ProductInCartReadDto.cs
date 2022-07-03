using System;

namespace WebApplication1.DTO
{
    public record ProductInCartReadDto
    {
        //public Guid Id { get; init; }
        public ProductReadDto Product { get; init; }
        public ChildCartReadDto Cart { get; init; }
        public int Quantity { get; init; }
    }
}
