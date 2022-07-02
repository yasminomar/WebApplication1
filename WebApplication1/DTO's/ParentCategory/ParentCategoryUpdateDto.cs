using System;
using System.Collections.Generic;

namespace WebApplication1.DTO
{
    public record ParentCategoryUpdateDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<Guid> CategoriesId { get; init; }

    }
}
