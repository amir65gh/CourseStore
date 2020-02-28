using CourseStore.Core.Domain;
using System.Collections.Generic;

namespace CourseStore.Core.Contract
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(long id);
        void Add(TEntity entity);
        List<TEntity> GetList();
        void Delete(TEntity entity);
        void Delete(long id);
        void Update(TEntity entity);
        void Save();

    }
}
