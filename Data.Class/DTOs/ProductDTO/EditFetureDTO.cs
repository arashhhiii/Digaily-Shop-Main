using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Data.Class.DTOs.ProductDTO
{
    public class EditFetureDTO
    {
        public long FetureId { get; set;  }
        public string Title { get; set; }
        public string Value { get; set; }
        public int Order { get; set;  }
    }
}
