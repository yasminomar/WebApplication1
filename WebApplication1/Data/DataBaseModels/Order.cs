using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("CartId")]
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public string Street { get; set; }
        public string Apartment { get; set; }
        public int zip { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Status { set; get; }



    }
}
