using Data.Class.entities.Common;
using Data.Class.entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.entities.OrderEntity
{
    public class OrderDetails:BaseEntity
    {
        public long ProductVarientId {  get; set; }
        public ProductVarient ProductVarient { get; set; }
    }
}
