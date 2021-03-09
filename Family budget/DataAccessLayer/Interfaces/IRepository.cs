using Family_budget.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        bool Delete(TEntity entity);

        Task<bool> DeleteAsync(int id);

        void DeleteRange(IQueryable<TEntity> queryable);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        IQueryable<TEntity> FindAll();

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> FindAllAsync();

        TEntity FindById(int id);

        Task<TEntity> FindByIdAsync(int id);
    }
}
