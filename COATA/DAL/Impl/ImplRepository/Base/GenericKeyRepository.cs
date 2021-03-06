﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Abstract.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.ImplRepository.Base
{
     public class GenericKeyRepository<TKey, TEntity> : IGenericKeyRepository<TKey, TEntity> where TEntity : class
    {
        public GenericKeyRepository(CoataDbContext context)
        {
            Context = context;
        }

        public CoataDbContext Context { get; }

        public DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var item = await Context.Set<TEntity>().AddAsync(entity);
            return item.Entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(0);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
             Context.Set<TEntity>().Remove(entity);
             return Task.CompletedTask;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetByAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await Context.Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<int> GetCountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }

        public virtual Task<int> GetCountAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().CountAsync(predicate);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync
            (Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<List<TEntity>> PagingFetchAsync
            (int startIndex, int count)
        {
            return await Context.Set<TEntity>().Skip(startIndex)
                .Take(count).ToListAsync();
        }

        public virtual async Task<List<TEntity>> PagingFetchByAsync
            (Expression<Func<TEntity, bool>> predicate, int startIndex, int count)
        {
            return await Context.Set<TEntity>().Where(predicate)
                .Skip(startIndex).Take(count).ToListAsync();
        }

        public virtual async Task<bool> IsExisting(TKey id)
        {
            TEntity res = await GetByIdAsync(id);
            return res != null;
        }
    }
}