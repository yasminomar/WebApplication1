using System;
using System.Collections.Generic;

namespace WebApplication1.Repository
{
    public interface IGenericRepository<TEntity>where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(Guid id);
        void Create(TEntity entity);
        void Delete(Guid id);
        int SaveChanges();
        void Update(TEntity entity);





    }
}