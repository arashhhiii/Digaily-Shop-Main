using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.DTOs.ProductCategoryDTO
{
    public class EditCategoryDTO
    {
        public long CategoryId { get; set; }
        public long ParentId { get; set; }
        public string title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}
