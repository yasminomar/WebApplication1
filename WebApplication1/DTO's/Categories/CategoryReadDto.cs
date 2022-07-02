using System;
using System.Collections.Generic;

namespace WebApplication1.DTO
{
    public record CategoryReadDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ChildParentCategoryReadDto ParentCategory { get; init; }
        public List<ChildProductReadDto> Products { get; init; }
    }
}
