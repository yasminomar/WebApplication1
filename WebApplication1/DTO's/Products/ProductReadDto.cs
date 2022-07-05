using System;

namespace WebApplication1.DTO
{
    public record ProductReadDto
    {
        public Guid Id { get; init; }
        public string Image { get; init; }
        public string ArabicName { get; init; }
        public string EnglishName { get; init; }
        public string Description { get; init; }
        public int UnitPrice { get; init; }
        public int Quantity { get; init; }

        public ChildCategoryReadDto Category { get; init; }
        public ChildVendorReadDto Vendor { get; init; }
    }
}
