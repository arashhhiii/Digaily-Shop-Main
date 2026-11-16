using Data.Class.entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.ProductEntities
{
    public class ProductGallery :BaseEntity
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public int Order {  get; set; }
        public string ImageName { get; set; }
    }
}
