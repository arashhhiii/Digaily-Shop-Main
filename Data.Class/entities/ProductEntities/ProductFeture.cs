using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class ProductFeture :BaseEntity
    {
        public string Title  { get; set; }
        public string value {  get; set; }
        public long ProductId { get; set; }
        public int Order {  get; set; }
        public Product Product { get; set; }
    }
}
