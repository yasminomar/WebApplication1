using System;

namespace WebApplication1.DTO
{
    public record TokenDto
    {
        public string Token { get; init; }
        public DateTime ExpiryDate { get; init; }

    }
}
