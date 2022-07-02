using System;
using System.Collections.Generic;

namespace WebApplication1.DTO
{
    public record ParentCategoryReadDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<ChildCategoryReadDto> Categories { get; init; }
    }
}
