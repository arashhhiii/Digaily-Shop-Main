using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class ProductCategory :BaseEntity
    {
        public long ParentId { get; set; }
        public string title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public ProductCategory Parent { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>(); 
        public ICollection<ProductSelectCategory> ProductSelectedCategory { get; set; }
    }
}
