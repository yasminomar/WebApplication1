using System;
using System.Collections.Generic;
using WebApplication1.DTO_s.User;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public record CartReadDto
    {
        public Guid Id { get; init; }
        public ChildApplicationUserReadDto ApplicationUser { get; init; }
        public List<ChildProductInCartReadDto> Products { get; init; }
    }
}
