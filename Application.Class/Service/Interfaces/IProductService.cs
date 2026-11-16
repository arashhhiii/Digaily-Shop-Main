using Data.Class.DTOs.ProductDTO;
using Data.Class.DTOs.ProductCategoryDTO;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Class.entities.ProductEntities;

namespace Application.Class.Service.Interfaces
{
    public interface IProductService:IAsyncDisposable
    {
        #region Product
        Task<List<ProductCategory>> GetAllProductCategory();
        Task<FilterProductDTO> FilterProduct(FilterProductDTO Filter);
            Task<ProductDetailsDTO> ProductDetails(ProductDetailsDTO Details);
        Task<CreateProductResult> CreateProductCreate(CreateProductDTO Create);
        Task<EditProductDTO> GetEditProductDTO(long ProductId);
        Task<EditProductResult> EditProduct(EditProductDTO dTO);
        Task<bool> DeleteProduct(long ProductId);

        #endregion
        #region Categories
        Task<bool> AddProductSelectedCategories(List<long> SelectedCategories, long ProductId);
        Task RemoveProductSelectedCategories(long ProductId);
        Task<bool> CreateCategory(CreateCategoryDTO dTO);
        Task<bool> EditCategory(EditCategoryDTO dTO);
        Task<EditCategoryDTO> GetCategory(long categoryId);
        Task<FilterCategoryDTO> FilterCategory(FilterCategoryDTO Filter);
        Task<bool> DeleteCategory(long CategoryId);
        #endregion
        #region Color
        Task<FilterColorDTO> FilterColor(FilterColorDTO dTO);
        Task<List<ProductColor>> GetAllProductColor();
        Task CreateColor(CreateColorDTO dTO);
        Task<EditColorDTO> GetEditColor(long ColorId);
        Task EditColor(EditColorDTO dTO);
        Task<bool> DeletColor(long ColorId);
        #endregion
        #region ProductVarient
        Task CreateProductVarient(CreateProductVarientDTO dTO);
        Task<EditProductVarientDTO> GetEditProductVarient(long VarientId);
        Task EditProductVarient(EditProductVarientDTO dTO);
        Task<bool> DeletProductVarient(long VarientId);
        #endregion
        #region Feture
        Task<bool> DeleteFeture(long fetureId);
        #endregion
        #region Gallery
        Task CreateGallery(CreateGalleryDTO dTO);
        Task<EditGalleryDTO> GetEditGallery(long GalleryId);
        Task EditGallery(EditGalleryDTO dTO);
        Task<bool> DeleteGallery(long GalleryId);
        Task<string?> ProductDetails(long productId);
        #endregion
        #region Brand
        Task<FilterBrandDTO> FilterBrand(FilterBrandDTO Filter);
        Task<bool> CreateBrand(CreateBrandDTO dto);
        Task<bool> EditBrand(EditBrandDTO dto);
        Task<EditBrandDTO> getEditBrand(long BrandId);
        Task<bool> DeleteBrand(long BrandId);
        Task<List<Brand>> GetAllBrand();
        Task<string?> EditCategory(long categoryId);

        #endregion
    }
}
