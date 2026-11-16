using Application.Class.Service.Interfaces;
using Data.Class.DTOs.ProductDTO;
using Data.Class.entities.ProductEntities;
using Application.Class.Utilies;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using JetBrains.Annotations;
using Data.Class.DTOs.ProductCategoryDTO;

namespace Web.Mvc.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        #region CTOR
        private readonly IProductService _productService;
        public ProductController(IProductService ProductService)
        {
            _productService = ProductService;
        }
        #endregion
        #region Product
        [HttpGet("Filter-Product")]
        public async Task<IActionResult> FilterProduct(FilterProductDTO Filter)
        {
            var model = await _productService.FilterProduct(Filter);
            return View(model);

        }
        [HttpGet("Prduct-Details-{productId}")]
        public async Task<IActionResult> ProductDetails(long productId)
        {
            var model = await _productService.ProductDetails(productId);
            return View(model);
        }
        [HttpGet("Create-Product")]

        public async Task<IActionResult> CreateProduct()
        {
            ViewData["Categories"] = await _productService.GetAllProductCategory();
            ViewData["Brand"] = await _productService.GetAllBrand();
            return View();
        }
        [HttpPost("Create-Product")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            ViewData["Categories"] = await _productService.GetAllProductCategory();
            ViewData["Brand"] = await _productService.GetAllBrand();
            if (!await CheckContentImage.IsValidAsync(dto.MainImageName))
            {
                TempData[ErrorMessage] = "the image format ios not valid";
                return View(dto);
            }
            if (dto.ProductGalllerys != null)
            {
                foreach (var gallery in dto.ProductGalllerys)
                {
                    if (!await CheckContentImage.IsValidAsync(gallery))
                    {
                        TempData[ErrorMessage] = "the desire image format is not corect";
                        return View(dto);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                var res = await _productService.CreateProductCreate(dto);
                switch (res)
                {

                    case CreateProductResult.Error:
                        TempData[ErrorMessage] = "The Opration has been down";
                        break;
                    case CreateProductResult.SavingMainImageFailed:
                        TempData[ErrorMessage] = "Saving image product have been failed";
                        break;
                    case CreateProductResult.BrandNotFound:
                        TempData[ErrorMessage] = "The Brand Not Found";
                        break;
                    case CreateProductResult.CategoryNotFound:
                        TempData[ErrorMessage] = "The Category not Found";
                        break;
                    case CreateProductResult.Successed:
                        TempData[SuccessMessage] = "The Operation was Successfull!";
                        return RedirectToAction("FilterProduct");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            TempData[ErrorMessage] = "The Opration has been down";
            return View();
        }

        [HttpGet("Edit-Product")]
        public async Task<IActionResult> EditProduct(long ProductId)
        {
            var model = await _productService.GetEditProductDTO(ProductId);

            return View();

        }
        [HttpPost("Edit-Product")]
        public async Task<IActionResult> EditProduct(EditProductDTO dto)
        {
            ViewData["Categories"] = await _productService.GetAllProductCategory();
            ViewData["Brand"] = await _productService.GetAllBrand();
            if (!await CheckContentImage.IsValidAsync(dto.MainImageName))
            {
                TempData[ErrorMessage] = "the image format ios not valid";
                return View(dto);
            }
            if (ModelState.IsValid)
            {
                var res = await _productService.EditProduct(dto);
                switch (res)
                {
                    case EditProductResult.Successed:
                        return RedirectToAction("ProductDetails", new { productId = dto.ProductId });
                        TempData[SuccessMessage] = "opratiomn was successfull";
                        break;
                    case EditProductResult.Error:
                        TempData[ErrorMessage] = "opration has been failed";
                        break;
                    case EditProductResult.SavingMainImageFailed:
                        TempData[ErrorMessage] = "the image have not been loaded";
                        break;
                    case EditProductResult.BrandNotFound:
                        TempData[ErrorMessage] = "Brand nopt found";
                        break;
                    case EditProductResult.CategoryNotFound:
                        TempData[ErrorMessage] = "category has not found ";
                        break;
                }
            }
            return View();




        }

        [Route("Delete-Product")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            var res = await _productService.DeleteProduct(productId);
            if (res)
            {
                TempData[SuccessMessage] = deletetext;
                return RedirectToAction("FilterProduct");
            }
            TempData[ErrorMessage] = "the product that have been buy out cant be deleted";
            return RedirectToAction("FilterProduct");
        }
        #endregion
        #region Category
        [HttpGet("Filter-Category")]
        public async Task<IActionResult> FilterCategory(FilterCategoryDTO dto)
        {
            var model = await _productService.FilterCategory(dto);
            return View(model);
        }
        [HttpGet("Create-Category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO dto)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategory();
            var data = await _productService.CreateCategory(dto);
            return View(data);
        }
        
        [HttpPost("Create-Category")]
        public async Task<IActionResult> CreateCategoryPost(CreateCategoryDTO dto)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategory();
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var res = await _productService.CreateCategory(dto);
            if (res)
            {
                TempData[SuccessMessage] = successtext;
                return RedirectToAction("FilterCategory");
            }
            TempData[ErrorMessage] = "The entered url is douplicated";
            return View(dto);
        }

        [HttpGet("Edit-Category")]
        public async Task<IActionResult> EditCategory(long CategoryId)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategory();
            var data = await _productService.EditCategory(CategoryId);
            return View(data);
        }

        [HttpPost("Edit-Category")]
        public async Task<IActionResult> EditCategoryPost(EditCategoryDTO dto)
        {
            ViewData["ParentCategories"] = await _productService.GetAllProductCategory();
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var res = await _productService.EditCategory(dto);
            if (res)
            {
                TempData[SuccessMessage] = successtext;
                return RedirectToAction("FilterCategory");
            }
            TempData[ErrorMessage] = "The entered url is douplicated";
            return View(dto);
        }
        [Route("Delete-Category")]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            var res = await _productService.DeleteCategory(categoryId);
            if (res)
            {
                TempData[SuccessMessage] = deletetext;
                return RedirectToAction("FilterCategory");
            }
            TempData[ErrorMessage] = "the category that comntainn a product cant be deleted";
            return RedirectToAction("FilterCategory");
        }
        #endregion
        #region Color
        [HttpGet("filter-Color")]
        public async Task<IActionResult> FilterColor(FilterColorDTO dto)
        {
            var model = await _productService.FilterColor(dto);
            return View(model);
        }
        [HttpGet("Create-Color")]
        public async Task<IActionResult> CreateColor()
        { 
            return View();
        }
        [HttpPost("Create-Color"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateColor(CreateColorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _productService.CreateColor(dto);
            TempData[SuccessMessage] = successtext;
            return View();
        }

       

        [HttpGet("Edit-Color")]
        public async Task<IActionResult> EditColor(long ColorId)
        {
            var model = await _productService.GetEditColor(ColorId);
            return View(model);
        }
        [HttpPost("Edit-Color"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditColor(EditColorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _productService.EditColor(dto);
            TempData[SuccessMessage] = successtext;
            return View();
        }


        [Route("Delete-Color")]
        public async Task<IActionResult> DeleteColor(long colorId)
        {
            var res = await _productService.DeletColor(colorId);
            if (res)
            {
                TempData[SuccessMessage] = successtext;
                return RedirectToAction("FilterColor");

            }
            TempData[ErrorMessage] = "the color that has been in the desire product cant be deletde";
            return RedirectToAction("FilterColor");
        }
        #endregion
        #region Fetures

        #endregion
    }
}
