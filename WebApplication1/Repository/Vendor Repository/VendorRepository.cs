using System;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class VendorRepository:GenericRepository<Vendor>, IVendorRepository
    {
        private readonly OnlineStoreContext _context;
        public VendorRepository(OnlineStoreContext context) : base(context)
        {
            _context = context;
        }

     
    }
}
