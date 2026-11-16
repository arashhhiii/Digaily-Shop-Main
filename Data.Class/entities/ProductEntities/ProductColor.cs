using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class ProductColor : BaseEntity
    {
        public string Title { get; set; }
    public string ColorCode { get; set; }
        public ICollection<ProductVarient> ProductVarients { get; set; }
    }
}
