using Data.Class.DTOs.Pagging;
using Data.Class.DTOs.ProductCategoryDTO;
using Data.Class.entities.Common;
using Data.Class.entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.DTOs.ProductDTO
{
    public class FilterColorDTO : Pager
    {
        public string Title { get; set; }
        public List<ProductColor> Data { get; set; }
        public FilterCategoryStatus CategoryStatus { get; set; }

        #region Methods
        public FilterColorDTO SetData(List<ProductColor> data)
        {
            Data = data;
            return this;
        }

        public FilterColorDTO SetPagging(BasePagging Paging)
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
