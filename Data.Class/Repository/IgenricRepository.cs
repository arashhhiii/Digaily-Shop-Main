using Data.Class.entities.Common;
using Data.Class.entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.Repository
{
    public interface IgenricRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity 
    {
        IQueryable<TEntity> GetQuery();
        Task<TEntity> GetEntityById(long Id);
        Task Addentity(TEntity entity);
        Task AddRangeEntity(List<TEntity> entities);
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        void DeleteEntity(List<TEntity> entity);
        Task DeletPermanent(TEntity entity);
        Task SaveAsync();
        void DeletPermanentEntity(List<ProductFeture> fetures);
        void DeletPermanentEntity(List<ProductComment> comment);
        Task AddEntity(ProductCategory category);
        Task AddEntity(ProductColor color);
        Task AddEntity(ProductGallery gallery);
        Task AddEntity(ProductVarient varient);
    }
}
