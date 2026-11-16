using Data.Class.DTOs.Pagging;
using Data.Class.DTOs.ProductCategoryDTO;
using Data.Class.entities.ProductEntities;

namespace Data.Class.DTOs.ProductDTO
{
    public class FilterBrandDTO : Pager
    {
        public string Title { get; set; }
        public string URl { get; set; }
        public List<Brand> Data { get; set; }
        public FilterCategoryStatus CategoryStatus { get; set; }

        #region Methods
        public FilterBrandDTO SetData(List<Brand> data)
        {
            Data = data;
            return this;
        }

        public FilterBrandDTO SetPagging(BasePagging Paging)
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
}
