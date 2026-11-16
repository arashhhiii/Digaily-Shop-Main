using Data.Class.DTOs.Pagging;
using Data.Class.entities.ProductEntities;
using System.Collections.Generic;

namespace Data.Class.DTOs.ProductCategoryDTO
{
    public class FilterCategoryDTO : Pager
    {
        public string Title { get; set; }
        public string URl {  get; set; }
        public long? CategoryId { get; set; }
        public List<ProductCategory> Data {  get; set; }
        public FilterCategoryStatus CategoryStatus { get; set; }

        #region Methods
        public FilterCategoryDTO SetData(List<ProductCategory> data)
        {
            Data = data;
            return this;
        }

        public FilterCategoryDTO SetPagging(BasePagging Paging)
        {
            PageId = Paging.Id;
            AllEntitiesCount = Paging.AllEntitiesCount;
            SatrtPage = Paging.StartPage;
            EndPage = Paging.EndPage;
            HowManyShopPageAfterAndBefore = Paging.HowManyShopPageAfterAndBefore;
            TakeEntity = Paging.TakeEntity;
            SkipEntity = Paging.SkipEntity;
            PageCount = Paging.PageCount;
            return this;
        }
        #endregion
    }
    public enum FilterCategoryStatus
    {
        All,
        Active,
        DiActive
    }
}
