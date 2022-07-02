using System;

namespace WebApplication1.DTO
{
    public record ChildParentCategoryReadDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
