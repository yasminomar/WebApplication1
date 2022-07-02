using System;
using System.Collections.Generic;

namespace WebApplication1.DTO
{
    public record VendorReadDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { init; get; }
        public string Address { init; get; }
        public string Mobile { get; init; }
        public List<ChildProductReadDto> Products { get; init; }

    }
}
