using Data.Class.entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.DTOs.ProductDTO
{
    public class ProductDetailsDTO
    {
        public long id
        {
            get;
            set;
        }
        public DateTime CreatedTime
        {
            get;
            set;
        }
        public DateTime UpdatedTime
        {
            get;
            set;
        }
        public bool isDelete
        {
            get;
            set;
        }
        public long BrandId { get; set; }
        public string title { get; set; }
        public string MainImageName { get; set; }
        public string Description { get; set; }
        public string ShortDecscription { get; set; }
        public bool IsAvailable { get; set; }
        public ProductSelectedBrand? ProductSelectedBrand { get; set; }
        
        public List<ProductSelectCategory> SelectedCategory { get; set; }
        public List<ProductComment> ProductComment { get; set; }
        public List<ProductVarient> ProductVarient { get; set; }
        public List<ProductGallery> ProductGallery { get; set; }
        public List<ProductFeture> ProductFeture { get; set; }
      
    }
}
