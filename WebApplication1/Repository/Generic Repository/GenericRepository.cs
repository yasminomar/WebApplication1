using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class
    {
        private readonly OnlineStoreContext _context;
        public GenericRepository(OnlineStoreContext context)
        {
            _context = context;
        }
        public void Create(TEntity entity)
        {
             _context.Set<TEntity>().Add(entity);
        }
        public void Delete(Guid id)
        {
            var entityToDelete=GetById(id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }
        }
        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public void Update(TEntity entity)
        {

        }
    }
}
