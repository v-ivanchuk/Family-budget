using Family_budget.DataAccessLayer.Entities;
using Family_budget.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Family_budget.DataAccessLayer.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : BaseEntity
    {
        protected readonly BudgetContext context;
        private readonly DbSet<TEntity> dbEntities;

        public Repository(BudgetContext context)
        {
            this.context = context;
            dbEntities = this.context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity) => dbEntities.Add(entity).Entity;

        public async Task<TEntity> AddAsync(TEntity entity) => (await dbEntities.AddAsync(entity)).Entity;

        public bool Delete(TEntity entity) => dbEntities.Remove(entity).Entity != null;

        public async Task<bool> DeleteAsync(TEntity entity) => 
            (await Task.Run(() => dbEntities.Remove(entity))).Entity != null;

        public TEntity Update(TEntity entity) => dbEntities.Update(entity).Entity;

        public async Task<TEntity> UpdateAsync(TEntity entity) => 
            (await Task.Run(() => dbEntities.Remove(entity))).Entity;

        public IQueryable<TEntity> FindAll()
        {
            return dbEntities;
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return dbEntities.Where(expression);
        }

        public TEntity GetById(int id)
        {
            return dbEntities.FirstOrDefault(entity => entity.Id == id);
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            return dbEntities.FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}
