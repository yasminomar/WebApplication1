using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public record CartWriteDto
    {
        public virtual ApplicationUser ApplicationUser { get; init; }
        //public List<Guid> ProductInCartsId { get; init; }
    }
}
