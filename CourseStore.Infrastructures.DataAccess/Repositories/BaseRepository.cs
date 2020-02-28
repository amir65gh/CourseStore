using CourseStore.Core.Contract;
using CourseStore.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseStore.Infrastructures.DataAccess.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IRepository<TEntity>
         where TEntity : BaseEntity, new()
         where TDbContext : DbContext
    {
        protected readonly TDbContext _dbContext;
        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public void Delete(long id)
        {
            var entity = GetById(id);
            if (entity != null)
                Delete(entity);
        }

        public virtual TEntity GetById(long id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
    }
}
