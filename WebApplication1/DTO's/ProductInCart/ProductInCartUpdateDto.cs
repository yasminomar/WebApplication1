using System;

namespace WebApplication1.DTO
{
    public record ProductInCartUpdateDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public Guid CartId { get; init; }
        public int Quantity { get; init; }

    }
}
