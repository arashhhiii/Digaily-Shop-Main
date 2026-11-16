using Data.Class.entities.Common;

namespace Data.Class.entities.ProductEntities
{
    public class ProductVarient:BaseEntity
    {
        public long colorId {  get; set; }
        public int Price {  get; set; }
        public int stockcount { get; set; }
        public ProductColor ProductColor { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
