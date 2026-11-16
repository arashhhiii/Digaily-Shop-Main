using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Class.DTOs.ProductDTO
{
    public class CreateGalleryDTO
    {
        public long ProductId {  get; set; }
        public IFormFile ImageName {  get; set; }
        public int Order {  get; set; }
    }
}
