using Microsoft.AspNetCore.Http;

namespace Data.Class.DTOs.ProductDTO
{
    public class EditBrandDTO
    {
         public long BrandId {  get; set; }
        public string Title { get; set; }
        public IFormFile? ImageName { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
    }
}
