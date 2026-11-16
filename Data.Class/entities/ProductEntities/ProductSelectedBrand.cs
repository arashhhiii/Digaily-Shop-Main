using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class ProductSelectedBrand :BaseEntity
    {
        public long? ProductId {  get; set; }
        public long? BrandId { get; set; }
        public Product Product { get; set; }
        public Brand Brand { get; set; }
    }
}
