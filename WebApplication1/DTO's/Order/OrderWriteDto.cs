using System;

namespace WebApplication1.DTO
{
    public record OrderWriteDto
    {
        public Guid CartId { get; init; }
        public string Street { get; init; }
        public string Apartment { get; init; }
        public int zip { get; init; }
        public string FirstName { init; get; }
        public string LastName { init; get; }
        public string Phone { init; get; }
        public string Email { init; get; }
        public string Status { init; get; }


    }
}
