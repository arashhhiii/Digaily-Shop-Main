using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.DTOs.ProductDTO
{
    public class CreateProductDTO 
    {
        public string title { get; set; }
        public IFormFile MainImageName { get; set; }
        public string Description { get; set; }
        public string ShortDecscription { get; set; }
        public bool IsAvailable { get; set; }
        public long BrandId {  get; set; }
        public int Order { get; set; }
        public List<long> Category {  get; set; }
        public List<IFormFile> ProductGalllerys { get; set; }
        public List<ProductFetureDTO>? ProductFetures { get; set; }
    }
    public enum CreateProductResult{
        Successed, 
        Error, 
        SavingMainImageFailed,
        BrandNotFound,
        CategoryNotFound

    }
}
