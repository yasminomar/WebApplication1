using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public record ProductUpdateDto
    {
        public Guid Id { get; init; }
        [Required]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Please Enter English characters")]
        public string EnglishName { get; init; }
        [Required]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669 ]+$", ErrorMessage = "Please Enter Arabic characters")]
        public string ArabicName { get; set; }
        public string Description { get; set; }
        [Range(1, 10000, ErrorMessage = "Enter number between 1 to 10000")]
        [Required]
        public int UnitPrice { get; set; }
        [Range(1, 10000, ErrorMessage = "Enter number between 1 to 10000")]
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid VendorId { get; set; }
        public string Image { get; set; }

    }
}
