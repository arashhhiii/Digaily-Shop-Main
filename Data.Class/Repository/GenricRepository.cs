using System;
using System.Collections.Generic;
using System.Text;
using Data.Class.entities.Common;
using Data.Class.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Class.Repository
{
    public class GenricRepository<TEntity> : IgenricRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public GenricRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this._dbSet = _dbContext.Set<TEntity>();
            
        }
        public async Task Addentity(TEntity entity)
        {
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeEntity(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedTime = DateTime.Now;
                entity.UpdatedTime = DateTime.Now;
                await _dbSet.AddAsync(entity);
            }
        }

        public void DeleteEntity(TEntity entity)
        {
            entity.isDelete = true;
            EditEntity(entity);
        }

        public void DeleteEntity(List<TEntity> entity)
        {
            foreach(var item in entity)
            {
                item.isDelete = true;
                EditEntity(item);
            }
        }

        public async Task DeletPermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async ValueTask DisposeAsync()
        {
            if (_dbContext != null)
            {
                await _dbContext.DisposeAsync();
            }
        }

        public void EditEntity(TEntity entity)
        {
            entity.UpdatedTime = DateTime.Now;
            _dbSet.Update(entity);
        }

        public async Task<TEntity> GetEntityById(long Id)
        {
            return await _dbSet.SingleOrDefaultAsync(d => d.id == Id);
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
