﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.InfrastructureBases
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        #region Vars / Props

        protected readonly AppDbContext _dbContext;

        #endregion

        #region Constructor(s)
        public GenericRepositoryAsync(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion


        #region Methods

        #endregion

        #region Actions
        public virtual async Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        public IQueryable<T> GetTableNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();

        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity is ISoftDeleteable softDeleteable)
            {
                softDeleteable.Delete();
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Set<T>().Remove(entity);
            }

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();

        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}