using Azure.Core.Pipeline;
using Data.Class.entities.Common;

namespace Data.Class.entities.ProductEntities
{
    public class Product :BaseEntity
    {
        #region Properties
        public long BrandId { get; set; }
        public string title { get; set; }
        public int Price {  get; set; }
        public string MainImageName { get; set; }
        public string Description { get; set; }
        public string ShortDecscription { get; set; }
        public bool IsAvailable { get; set; }
        public ProductCategory Category { get; set; }
        public ProductSelectedBrand? ProductSelectedBrand { get; set; }
        #endregion
        #region Relation
        public ICollection<ProductSelectCategory> SelectedCategory { get; set; }
        public ICollection<ProductComment> ProductComment { get; set; }
        public ICollection<ProductVarient> ProductVarient {  get; set; }
        public ICollection<ProductGallery> ProductGallery { get; set; }
        public ICollection<ProductFeture> ProductFeture { get; set; }
        #endregion
    }
}
