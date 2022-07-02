using System;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class OrderReadDto
    {
        public Guid Id { get; init; }
        public virtual ApplicationUser ApplicationUser { get; init; }
        public ChildCartReadDto Cart { get; init; }

        public string Street { get; init; }
        public string Apartment { get; init; }
        public int zip { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
        public string Status { get; init; }


    }
}
