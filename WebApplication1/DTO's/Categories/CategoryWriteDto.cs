using System;
using System.Collections.Generic;
namespace WebApplication1.DTO
{
    public record CategoryWriteDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public Guid ParentCategoryId { get; init; }
        //public List<Guid> ProductsId { get; init; }

    }
}
