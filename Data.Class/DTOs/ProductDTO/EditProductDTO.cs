using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.DTOs.ProductDTO
{
    public class EditProductDTO
    {
        public long ProductId { get; set; }
        public string title { get; set; }
        public IFormFile? MainImageName { get; set; }
        public string Description { get; set; }
        public string ShortDecscription { get; set; }
        public bool IsAvailable { get; set; }
        public long BrandId { get; set; }
        public List<long> Category { get; set; }
        public List<ProductFetureDTO>? ProductFetures { get; set; }
    }
    public enum EditProductResult
    {
        Successed,
        Error,
        SavingMainImageFailed,
        BrandNotFound,
        CategoryNotFound
       

    }
}
