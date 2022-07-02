using System;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class ParentCategoryRepository:GenericRepository<ParentCategory>, IParentCategoryRepository
    {
        private readonly OnlineStoreContext _context;
        public ParentCategoryRepository(OnlineStoreContext context) : base(context)
        {
            _context = context;
        }


    }
}
