using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.DTOs.ProductDTO
{
    public class CreateProductVarientDTO
    {
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public int Price { get; set; }
        public int StockCount { get; set; }
    }
}
