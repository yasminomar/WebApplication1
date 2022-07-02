using System;

namespace WebApplication1.DTO
{
    public record ChildProductReadDto
    {
        public Guid Id { get; init; }
        public string EnglishName { get; init; }
        public string ArabicName { get; init; }
    }
}
