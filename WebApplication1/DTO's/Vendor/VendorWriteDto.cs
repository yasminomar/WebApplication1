using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public record VendorWriteDto
    {
        public string Name { get; init; }
        public string Email { init; get; }
        public string Address { init; get; }

        [RegularExpression(@"^(\+201|01|00201)[0-2,5]{1}[0-9]{8}", ErrorMessage = "Not a valid phone number")]
        public string Mobile { get; init; }
        //public List<Guid> ProductsId { get; init; }
    }
}
