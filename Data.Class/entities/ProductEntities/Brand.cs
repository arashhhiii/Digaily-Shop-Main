using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class Brand : BaseEntity
    {
        public string Title { get; set; }
        public string ImageName {  get; set; }
        public string Url {  get; set; }
        public int Order { get; set; }
        public ICollection<ProductSelectedBrand> ProductSelectedBrands { get; set; }

    }
}
