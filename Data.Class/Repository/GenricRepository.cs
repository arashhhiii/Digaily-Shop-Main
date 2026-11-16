using System;
using System.Collections.Generic;
using System.Text;
using Data.Class.entities.Common;
using Data.Class.entities.ProductEntities;
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

        public void DeletPermanentEntity(List<ProductFeture> fetures)
        {
            _dbContext.Set<ProductFeture>().RemoveRange(fetures);
        }

        public void DeletPermanentEntity(List<ProductComment> comments)
        {
            _dbContext.Set<ProductComment>().RemoveRange(comments);
        }

        public async Task AddEntity(ProductCategory category)
        {
            category.CreatedTime = DateTime.Now;
            category.UpdatedTime = DateTime.Now;
            await _dbContext.Set<ProductCategory>().AddAsync(category);
        }

        public async Task AddEntity(ProductColor color)
        {
            color.CreatedTime = DateTime.Now;
            color.UpdatedTime = DateTime.Now;
            await _dbContext.Set<ProductColor>().AddAsync(color);
        }

        public async Task AddEntity(ProductGallery gallery)
        {
            gallery.CreatedTime = DateTime.Now;
            gallery.UpdatedTime = DateTime.Now;
            await _dbContext.Set<ProductGallery>().AddAsync(gallery);
        }

        public async Task AddEntity(ProductVarient varient)
        {
            varient.CreatedTime = DateTime.Now;
            varient.UpdatedTime = DateTime.Now;
            await _dbContext.Set<ProductVarient>().AddAsync(varient);
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
