using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class ProductSelectCategory:BaseEntity
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public Product Product { get; set; }
        public ProductCategory ProductCategory {  get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public long ParentId { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public string SubCategory { get; set; }
    }
}
