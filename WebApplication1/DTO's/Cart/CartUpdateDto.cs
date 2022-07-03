using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public record CartUpdateDto
    {
        public Guid Id { get; init; }
       // public virtual ApplicationUser ApplicationUser { get; init; }
        public List<Guid> ProductsId { get; init; }


    }
}
