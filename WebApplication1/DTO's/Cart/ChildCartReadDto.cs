using System;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public record ChildCartReadDto
    {
        public Guid Id { get; init; }
        //public virtual ApplicationUser ApplicationUser { get; init; }
    }
}
