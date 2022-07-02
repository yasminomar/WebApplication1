using System;

namespace WebApplication1.DTO
{
    public record ChildCategoryReadDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }

    }
}
