using Application.Class.Extension;
using Application.Class.Service.Interfaces;
using Data.Class.DTOs.Pagging;
using Data.Class.DTOs.ProductCategoryDTO;
using Data.Class.DTOs.ProductDTO;
using Data.Class.entities.OrderEntity;
using Data.Class.entities.ProductEntities;
using Data.Class.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Application.Class.Service.Implamentations
{
    public class ProductService : IProductService
    {
        #region CTOR
        private readonly IgenricRepository<Product> _ProductRepositoiry;
        private readonly IgenricRepository<ProductCategory> _ProductSelectedRepository;
        private readonly IgenricRepository<ProductColor> _ProductColorRepositoiry;
        private readonly IgenricRepository<ProductSelectedBrand> _ProductSelectBrandRepository;
        private readonly IgenricRepository<Brand> _BrandRepository;
        private readonly IgenricRepository<ProductVarient> _ProductVarientRepositoiry;
        private readonly IgenricRepository<ProductComment> _ProductCommentRepositoiry;
        private readonly IgenricRepository<ProductSelectCategory> _ProductSelectedRepositoiry;
        private readonly IgenricRepository<ProductFeture> _ProductFetureRepositoiry;
        private readonly IgenricRepository<ProductGallery> _ProductGalleryRepositoiry;
        private readonly IgenricRepository<OrderDetails> _OrderDetailsRepository;
        public ProductService(IgenricRepository<Product> ProductRepository, IgenricRepository<ProductCategory> ProductSelectedCategoryRepository, IgenricRepository<ProductColor> ProductColorRepository, IgenricRepository<ProductSelectedBrand> ProductSelectBrandRepository, IgenricRepository<Brand> BrandRepository, IgenricRepository<ProductVarient> ProductVarientRepository, IgenricRepository<ProductComment> ProductCommentRepository, IgenricRepository<ProductSelectCategory> ProductCategoryRepository, IgenricRepository<ProductFeture> ProductFutureRepository, IgenricRepository<ProductGallery> ProductGalleryRepository, IgenricRepository<OrderDetails> OrderDetailsRepository)
        {
            _ProductRepositoiry = ProductRepository;
            _ProductSelectedRepository = ProductSelectedCategoryRepository;
            _ProductColorRepositoiry = ProductColorRepository;
            _ProductSelectBrandRepository = ProductSelectBrandRepository;
            _BrandRepository = BrandRepository;
            _ProductVarientRepositoiry = ProductVarientRepository;
            _ProductCommentRepositoiry = ProductCommentRepository;
            _ProductSelectedRepositoiry = ProductCategoryRepository;
            _ProductFetureRepositoiry = ProductFutureRepository;
            _ProductGalleryRepositoiry = ProductGalleryRepository;
            _OrderDetailsRepository = OrderDetailsRepository;


        }
        public async ValueTask DisposeAsync()
        {
            await _ProductRepositoiry.DisposeAsync();
            await _ProductSelectedRepository.DisposeAsync();
            await _ProductColorRepositoiry.DisposeAsync();
            await _ProductSelectBrandRepository.DisposeAsync();
            await _BrandRepository.DisposeAsync();
            await _ProductVarientRepositoiry.DisposeAsync();
            await _ProductCommentRepositoiry.DisposeAsync();
            await _ProductSelectedRepositoiry.DisposeAsync();
            await _ProductFetureRepositoiry.DisposeAsync();
            await _ProductGalleryRepositoiry.DisposeAsync();
            await _OrderDetailsRepository.DisposeAsync();

        }

        public Task<ProductDetailsDTO> ProductDetails(ProductDetailsDTO details)
        {
            ArgumentNullException.ThrowIfNull(details);
            return ProductDetails(details.id);
        }


        #endregion
        #region Product

        public async Task<ProductDetailsDTO> ProductDetails(long ProductId)
        {
            var data = await _ProductRepositoiry.GetQuery()
                .FirstOrDefaultAsync(product => product.id == ProductId && !product.isDelete);
            if (data == null)
                return null!;

            return new ProductDetailsDTO
            {
                isDelete = data.isDelete,
                UpdatedTime = data.UpdatedTime,
                ProductVarient = await _ProductVarientRepositoiry.GetQuery().Where(item => item.Product.id == ProductId).ToListAsync(),
                CreatedTime = data.CreatedTime,
                Description = data.Description,
                id = data.id,
                IsAvailable = data.IsAvailable,
                ProductSelectedBrand = await _ProductSelectBrandRepository.GetQuery().FirstOrDefaultAsync(d => d.ProductId == ProductId),
                SelectedCategory = await _ProductSelectedRepositoiry.GetQuery().Where(d => d.ProductId == ProductId).ToListAsync(),
                ProductGallery = await _ProductGalleryRepositoiry.GetQuery().Where(d => d.ProductId == ProductId).ToListAsync(),
                ProductComment = await _ProductCommentRepositoiry.GetQuery().Where(d => d.ProductId == ProductId).ToListAsync(),
                ShortDecscription = data.ShortDecscription,
                ProductFeture = await _ProductFetureRepositoiry.GetQuery().Where(d => d.ProductId == ProductId).ToListAsync(),
                MainImageName = data.MainImageName,
                title = data.title,
                BrandId = await _ProductSelectBrandRepository.GetQuery()
                    .Where(link => link.ProductId == ProductId)
                    .Select(link => link.BrandId ?? 0)
                    .FirstOrDefaultAsync(),
            };
        }

        public async Task RemoveProductSelectedCategories(long ProductId)
        {
            var links = await _ProductSelectedRepositoiry.GetQuery()
                .Where(link => link.ProductId == ProductId)
                .ToListAsync();
            if (links.Count == 0)
                return;

            foreach (var link in links)
                await _ProductSelectedRepositoiry.DeletPermanent(link);
            await _ProductSelectedRepositoiry.SaveAsync();
        }
        public async Task<bool> AddProductSelectedCategories(List<long> SelectedCategories, long ProductId)
        {
            if (ProductId <= 0)
                return false;

            var product = await _ProductRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == ProductId && !item.isDelete);
            if (product == null)
                return false;

            var categoryIds = SelectedCategories?.Distinct().ToList() ?? [];
            if (categoryIds.Count == 0)
                return true;

            var categories = await _ProductSelectedRepository.GetQuery()
                .Where(category => categoryIds.Contains(category.id) && !category.isDelete)
                .ToListAsync();
            if (categories.Count != categoryIds.Count)
                return false;

            var existingCategoryIds = await _ProductSelectedRepositoiry.GetQuery()
                .Where(link => link.ProductId == ProductId && categoryIds.Contains(link.CategoryId))
                .Select(link => link.CategoryId)
                .ToListAsync();

            foreach (var category in categories.Where(category => !existingCategoryIds.Contains(category.id)))
            {
                await _ProductSelectedRepositoiry.Addentity(new ProductSelectCategory
                {
                    Product = product,
                    ProductId = ProductId,
                    CategoryId = category.id,
                    ProductCategory = category
                });
            }

            await _ProductSelectedRepositoiry.SaveAsync();
            return true;
        }
        public async Task<CreateProductResult> CreateProductCreate(CreateProductDTO dTO)
        {
            if (dTO == null || dTO.MainImageName == null || dTO.MainImageName.Length == 0)
            {
                return CreateProductResult.SavingMainImageFailed;
            }

            var selectedCategoryIds = dTO.Category?.Distinct().ToList() ?? [];
            var categories = await _ProductSelectedRepository.GetQuery()
                .Where(category => selectedCategoryIds.Contains(category.id) && !category.isDelete)
                .ToListAsync();
            if (categories.Count != selectedCategoryIds.Count)
                return CreateProductResult.CategoryNotFound;

            Brand? brand = null;
            if (dTO.BrandId > 0)
            {
                brand = await _BrandRepository.GetQuery()
                    .FirstOrDefaultAsync(item => item.id == dTO.BrandId && !item.isDelete);
                if (brand == null)
                    return CreateProductResult.BrandNotFound;
            }

            var product = new Product
            {
                title = dTO.title,
                Description = dTO.Description,
                ShortDecscription = dTO.ShortDecscription,
                IsAvailable = dTO.IsAvailable,
                BrandId = dTO.BrandId,

            };
            #region Main image
            try
            {
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                product.MainImageName = await dTO.MainImageName.UploadImageAsync(
                    webRootPath,
                    Path.Combine("images", "products"),
                    maxWidth: 150,
                    maxHeight: 150);
            }
            catch (Exception)
            {
                return CreateProductResult.SavingMainImageFailed;
            }
            #endregion
            await _ProductRepositoiry.Addentity(product);
            await _ProductRepositoiry.SaveAsync();
            #region Categories
            var addCategoryResult = await AddProductSelectedCategories(dTO.Category, product.id);
            if (!addCategoryResult) return CreateProductResult.CategoryNotFound;
            #endregion
            #region Brand
            if (brand != null)
            {
                var selectedBrand = new ProductSelectedBrand
                {
                    Product = product,
                    Brand = brand,
                    BrandId = brand.id,
                    ProductId = product.id,
                };
                await _ProductSelectBrandRepository.Addentity(selectedBrand);
                await _ProductSelectBrandRepository.SaveAsync();
            }
            #endregion
            #region Gallery
            if (dTO.ProductGalllerys != null && dTO.ProductGalllerys.Any())
            {
                var galleryOrder = 1;
                foreach (var item in dTO.ProductGalllerys.Where(file => file != null && file.Length > 0))
                {
                    try
                    {
                        var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        var galleryImagePath = await item.UploadImageAsync(
                            webRootPath,
                            Path.Combine("images", "products"),
                            maxWidth: 150,
                            maxHeight: 150);
                        await _ProductGalleryRepositoiry.Addentity(new ProductGallery
                        {
                            ProductId = product.id,
                            ImageName = galleryImagePath,
                            Order = galleryOrder++
                        });
                    }
                    catch (Exception)
                    {
                        return CreateProductResult.SavingMainImageFailed;
                    }
                }
                await _ProductGalleryRepositoiry.SaveAsync();
            }
            #endregion
            #region Fetures
            if (dTO.ProductFetures != null && dTO.ProductFetures.Any())
            {
                var FetureOrder = 1;
                foreach (var Item in dTO.ProductFetures)
                {
                    var Feture = new ProductFeture
                    {
                        Product = product,
                        ProductId = product.id,
                        value = Item.Value,
                        Title = Item.Title,
                        Order = FetureOrder,

                    };
                    await _ProductFetureRepositoiry.Addentity(Feture);
                    FetureOrder++;
                }
                await _ProductFetureRepositoiry.SaveAsync();
            }
            #endregion
            return CreateProductResult.Successed;
        }
        public async Task<bool> DeleteProduct(long ProductId)
        {
            var product = await _ProductRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == ProductId && !item.isDelete);
            if (product == null)
                return false;

            var isOrdered = await _OrderDetailsRepository.GetQuery()
                .AnyAsync(orderDetail => orderDetail.ProductVarient.ProductId == ProductId);
            if (isOrdered)
                return false;

            var features = await _ProductFetureRepositoiry.GetQuery()
                .Where(feature => feature.ProductId == ProductId)
                .ToListAsync();
            foreach (var feature in features)
                await _ProductFetureRepositoiry.DeletPermanent(feature);
            await _ProductFetureRepositoiry.SaveAsync();

            var categoryLinks = await _ProductSelectedRepositoiry.GetQuery()
                .Where(link => link.ProductId == ProductId)
                .ToListAsync();
            foreach (var categoryLink in categoryLinks)
                await _ProductSelectedRepositoiry.DeletPermanent(categoryLink);
            await _ProductSelectedRepositoiry.SaveAsync();

            var brandLinks = await _ProductSelectBrandRepository.GetQuery()
                .Where(link => link.ProductId == ProductId)
                .ToListAsync();
            foreach (var brandLink in brandLinks)
                await _ProductSelectBrandRepository.DeletPermanent(brandLink);
            await _ProductSelectBrandRepository.SaveAsync();

            var variants = await _ProductVarientRepositoiry.GetQuery()
                .Where(variant => variant.Product.id == ProductId)
                .ToListAsync();
            foreach (var variant in variants)
                await _ProductVarientRepositoiry.DeletPermanent(variant);
            await _ProductVarientRepositoiry.SaveAsync();

            var comments = await _ProductCommentRepositoiry.GetQuery()
                .Where(comment => comment.ProductId == ProductId)
                .ToListAsync();
            foreach (var comment in comments)
                await _ProductCommentRepositoiry.DeletPermanent(comment);
            await _ProductCommentRepositoiry.SaveAsync();

            var gallery = await _ProductGalleryRepositoiry.GetQuery()
                .Where(item => item.ProductId == ProductId)
                .ToListAsync();
            foreach (var item in gallery)
                item.ImageName?.DeleteImage();
            foreach (var item in gallery)
                await _ProductGalleryRepositoiry.DeletPermanent(item);
            await _ProductGalleryRepositoiry.SaveAsync();

            product.MainImageName?.DeleteImage();
            _ProductRepositoiry.DeleteEntity(product);
            await _ProductRepositoiry.SaveAsync();
            return true;
        }
        public async Task<EditProductResult> EditProduct(EditProductDTO dTO)
        {
            if (dTO == null)
                return EditProductResult.Error;

            var product = await _ProductRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.ProductId && !item.isDelete);
            if (product == null)
                return EditProductResult.Error;

            var categoryIds = dTO.Category?.Distinct().ToList() ?? [];
            var categories = await _ProductSelectedRepository.GetQuery()
                .Where(category => categoryIds.Contains(category.id) && !category.isDelete)
                .ToListAsync();
            if (categories.Count != categoryIds.Count)
                return EditProductResult.CategoryNotFound;

            Brand? brand = null;
            if (dTO.BrandId > 0)
            {
                brand = await _BrandRepository.GetQuery()
                    .FirstOrDefaultAsync(item => item.id == dTO.BrandId && !item.isDelete);
                if (brand == null)
                    return EditProductResult.BrandNotFound;
            }

            if (dTO.MainImageName is { Length: > 0 })
            {
                try
                {
                    var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    product.MainImageName = await dTO.MainImageName.UploadImageAsync(
                        webRootPath,
                        Path.Combine("images", "products"),
                        maxWidth: 150,
                        maxHeight: 150);
                }
                catch (Exception)
                {
                    return EditProductResult.SavingMainImageFailed;
                }
            }

            product.title = dTO.title;
            product.Description = dTO.Description;
            product.ShortDecscription = dTO.ShortDecscription;
            product.IsAvailable = dTO.IsAvailable;
            product.BrandId = dTO.BrandId;

            _ProductRepositoiry.EditEntity(product);
            await _ProductRepositoiry.SaveAsync();

            var oldBrandLinks = await _ProductSelectBrandRepository.GetQuery()
                .Where(link => link.ProductId == product.id)
                .ToListAsync();
            foreach (var oldBrandLink in oldBrandLinks)
                await _ProductSelectBrandRepository.DeletPermanent(oldBrandLink);
            if (brand != null)
            {
                await _ProductSelectBrandRepository.Addentity(new ProductSelectedBrand
                {
                    Product = product,
                    Brand = brand,
                    BrandId = brand.id,
                    ProductId = product.id
                });
            }
            await _ProductSelectBrandRepository.SaveAsync();
            #region Category
            await RemoveProductSelectedCategories(dTO.ProductId);
            var addCategoryResult = await AddProductSelectedCategories(categoryIds, dTO.ProductId);
            if (!addCategoryResult) return EditProductResult.CategoryNotFound;

            #endregion
            if (dTO.ProductFetures != null)
            {
                var existingFeatures = await _ProductFetureRepositoiry.GetQuery()
                    .Where(feature => feature.ProductId == product.id)
                    .ToListAsync();
                foreach (var feature in existingFeatures)
                    await _ProductFetureRepositoiry.DeletPermanent(feature);

                var order = 1;
                foreach (var featureDto in dTO.ProductFetures)
                {
                    await _ProductFetureRepositoiry.Addentity(new ProductFeture
                    {
                        Product = product,
                        ProductId = product.id,
                        Title = featureDto.Title,
                        value = featureDto.Value,
                        Order = order++
                    });
                }
                await _ProductFetureRepositoiry.SaveAsync();
            }

            return EditProductResult.Successed;
        }
        public async Task<FilterProductDTO> FilterProduct(FilterProductDTO Filter)
        {
            var query = _ProductRepositoiry.GetQuery().Include(d => d.SelectedCategory).Include(d => d.ProductVarient).ThenInclude(d => d.ProductColor).
                OrderByDescending(d => d.CreatedTime).AsQueryable();
            #region Switch
            switch (Filter.ProductStatus)
            {
                case FilterProductStatus.All:
                    break;
                case FilterProductStatus.Active:
                    query = query.Where(d => d.ProductVarient.Any(d => d.stockcount > 0));
                    break;
                case FilterProductStatus.DiActive:
                    query = query.Where(d => !d.ProductVarient.Any(d => d.stockcount > 0));
                    break;
                case FilterProductStatus.Available:
                    query = query.Where(d => d.IsAvailable);
                    break;
                case FilterProductStatus.NotAvailble:
                    query = query.Where(d => !d.IsAvailable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
            ;
            switch (Filter.ProductOrder)
            {
                case FilterProductOrder.Newest:
                    query = query.OrderByDescending(d => d.CreatedTime);
                    break;
                case FilterProductOrder.Oldest:
                    query = query.OrderBy(d => d.CreatedTime);
                    break;
                case FilterProductOrder.Expensive:
                    query = query.OrderByDescending(d => d.ProductVarient.OrderByDescending(d => d.Price));
                    break;
                case FilterProductOrder.Cheap:
                    query = query.OrderBy(d => d.ProductVarient.OrderBy(d => d.Price));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ;
            #endregion
            #region Filter
            if (!string.IsNullOrEmpty(Filter.title))
            {
                query = query.Where(d => EF.Functions.Like(d.title, $"{Filter.title}"));
            }
            if (Filter.StartPrice != null)
            {

                query = query.Where(d => d.Price < Filter.StartPrice);

            }
            if (Filter.endPrice != null)
            {
                query = query.Where(d => d.Price < Filter.endPrice);
            }
            if (Filter is { StartPrice: not null, endPrice: not null })
            {
                query = query.Where(d => d.Price < Filter.endPrice && d.Price > Filter.StartPrice);
            }
            if (query.Any())
            {
                Filter.MostExpensiveProice = query.OrderByDescending(d => d.Price).First().Price;
                Filter.MinExpensivePrice = query.OrderBy(d => d.Price).First().Price;
            }
            if (Filter.categoryId is > 0)
            {
                query = query.Where(d => d.SelectedCategory.Any(e => e.CategoryId == Filter.categoryId));



            }
            if (Filter.ColorId is > 0)
            {
                query = query.Where(d => d.ProductVarient.Any(s => s.colorId == Filter.ColorId));
            }
            if (Filter.BrandId is > 0)
            {
                query = query.Where(d => d.ProductSelectedBrand != null && d.ProductSelectedBrand.BrandId == Filter.BrandId);
            }

            #endregion
            #region Pagging
            var pagger = Pager.Build(Filter.PageId, await query.CountAsync());
            var AllEntities = await query.Pagging(pagger).ToListAsync();

            #endregion
            return Filter.SetData(AllEntities).SetPagging(pagger);

        }


        public async Task<EditProductDTO> GetEditProductDTO(long ProductId)
        {
            var data = await _ProductRepositoiry.GetQuery()
                .FirstOrDefaultAsync(product => product.id == ProductId && !product.isDelete);
            if (data == null)
                return null!;

            var brand = await _ProductSelectBrandRepository.GetQuery()
                .FirstOrDefaultAsync(link => link.ProductId == ProductId);
            return new EditProductDTO
            {
                BrandId = brand?.BrandId ?? 0,
                Description = data.Description,
                IsAvailable = data.IsAvailable,
                title = data.title,
                ProductId = ProductId,
                Category = await _ProductSelectedRepositoiry.GetQuery()
                    .Where(link => link.ProductId == ProductId)
                    .Select(link => link.CategoryId)
                    .ToListAsync()
            };
        }
        #endregion

        #region Category
        public async Task<bool> CreateCategory(CreateCategoryDTO dTO)
        {
            ArgumentNullException.ThrowIfNull(dTO);
            if (string.IsNullOrWhiteSpace(dTO.title) || string.IsNullOrWhiteSpace(dTO.Url))
                throw new ArgumentException("Category title and URL are required.", nameof(dTO));

            ProductCategory? parent = null;
            if (dTO.ParentId > 0)
            {
                parent = await _ProductSelectedRepository.GetQuery()
                    .FirstOrDefaultAsync(category => category.id == dTO.ParentId && !category.isDelete);
                if (parent == null)
                    throw new ArgumentException("The parent category does not exist.", nameof(dTO));
            }

            await _ProductSelectedRepository.AddEntity(new ProductCategory
            {
                ParentId = dTO.ParentId,
                Parent = parent,
                title = dTO.title.Trim(),
                Url = dTO.Url.Trim(),
                IsActive = dTO.IsActive,
                Order = dTO.Order
            });
            await _ProductSelectedRepository.SaveAsync();
            #region Check URl
            var UrlInUse = await _ProductSelectedRepositoiry.GetQuery().AnyAsync(d => d.Url == dTO.Url);
            if (UrlInUse) return false;
            #endregion
            var category = new ProductCategory
            {
                title = dTO.title,
                IsActive = true,
                Order = dTO.Order,
                ParentId = dTO.ParentId,
                Url = dTO.Url,

            };
            await _ProductSelectedRepositoiry.AddEntity(category);
            await _ProductSelectedRepository.SaveAsync();
            return true;
        }


        public async Task<bool> DeleteCategory(long CategoryId)
        {
            var category = await _ProductSelectedRepository.GetQuery()
                .FirstOrDefaultAsync(item => item.id == CategoryId && !item.isDelete);
            if (category == null)
                return false;

            var hasProducts = await _ProductSelectedRepositoiry.GetQuery()
                .AnyAsync(link => link.CategoryId == CategoryId);
            var hasChildren = await _ProductSelectedRepository.GetQuery()
                .AnyAsync(item => item.ParentId == CategoryId && !item.isDelete);
            if (hasProducts || hasChildren)
                return false;

            _ProductSelectedRepository.DeleteEntity(category);
            await _ProductSelectedRepository.SaveAsync();
            return true;

        }



        public async Task<bool> EditCategory(EditCategoryDTO dTO)
        {
            ArgumentNullException.ThrowIfNull(dTO);
            var category = await _ProductSelectedRepository.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.CategoryId && !item.isDelete);
            if (category == null)
                throw new KeyNotFoundException("Category was not found.");
            if (dTO.CategoryId == dTO.ParentId)
                throw new ArgumentException("A category cannot be its own parent.", nameof(dTO));
            if (string.IsNullOrWhiteSpace(dTO.title) || string.IsNullOrWhiteSpace(dTO.Url))
                throw new ArgumentException("Category title and URL are required.", nameof(dTO));

            ProductCategory? parent = null;
            if (dTO.ParentId > 0)
            {
                parent = await _ProductSelectedRepository.GetQuery()
                    .FirstOrDefaultAsync(item => item.id == dTO.ParentId && !item.isDelete);
                if (parent == null)
                    throw new ArgumentException("The parent category does not exist.", nameof(dTO));
            }

            category.ParentId = dTO.ParentId;
            category.Parent = parent;
            category.title = dTO.title.Trim();
            category.Url = dTO.Url.Trim();
            category.IsActive = dTO.IsActive;
            category.Order = dTO.Order;
            _ProductSelectedRepository.EditEntity(category);
            await _ProductSelectedRepository.SaveAsync();
            #region Check URl
            var UrlInUse = await _ProductSelectedRepositoiry.GetQuery().AnyAsync(d => d.Url == dTO.Url);
            if (UrlInUse) return false;
            #endregion
            var data = await _ProductSelectedRepositoiry.GetEntityById(dTO.CategoryId);
            data.Title = dTO.title;
            data.Url = dTO.Url;
            data.ParentId = dTO.ParentId;
            data.Order = dTO.Order;
            data.IsActive = dTO.IsActive;
            _ProductSelectedRepositoiry.EditEntity(data);
            await _ProductSelectedRepositoiry.SaveAsync();
            return true;
        }


        public async Task<FilterCategoryDTO> FilterCategory(FilterCategoryDTO Filter)
        {
            ArgumentNullException.ThrowIfNull(Filter);
            #region Query
            var query = _ProductSelectedRepository.GetQuery().OrderByDescending(d => d.CreatedTime).AsQueryable();

            #endregion
            #region Switch
            switch (Filter.CategoryStatus)
            {
                case FilterCategoryStatus.Active:
                    query = query.Where(d => d.IsActive);
                    break;
                case FilterCategoryStatus.All:
                    break;
                case FilterCategoryStatus.DiActive:
                    query = query.Where(d => !d.IsActive);
                    break;
                default:
                    throw new ArgumentException();

            }
            #endregion
            #region Filter
            if (!string.IsNullOrEmpty(Filter.Title))
            {
                query = query.Where(d => EF.Functions.Like(d.title, $"{Filter.Title}"));
            }
            #endregion
            #region Pagging
            var pagger = Pager.Build(Filter.PageId, await query.CountAsync());
            var AllEntities = await query.Pagging(pagger).ToListAsync();

            #endregion
            return Filter.SetData(AllEntities).SetPagging(pagger);
        }



        public async Task<EditCategoryDTO> GetCategory(long categoryId)
        {
            var category = await _ProductSelectedRepository.GetQuery()
                .FirstOrDefaultAsync(item => item.id == categoryId && !item.isDelete);
            if (category == null)
                return null!;

            return new EditCategoryDTO
            {
                CategoryId = category.id,
                ParentId = category.ParentId,
                title = category.title,
                Url = category.Url,
                IsActive = category.IsActive,
                Order = category.Order
            };
            var data = await _ProductSelectedRepository.GetEntityById(categoryId);
            return new EditCategoryDTO
            {
                title = category.title,
                Order = data.Order,
                Url = data.Url,
                CategoryId = categoryId,
                IsActive = category.IsActive,
                ParentId = category.ParentId,

            };
        }
        #endregion
        #region Color
        public async Task<FilterColorDTO> FilterColor(FilterColorDTO Filter)
        {
            ArgumentNullException.ThrowIfNull(Filter);
            var query = _ProductColorRepositoiry.GetQuery().OrderByDescending(d => d.CreatedTime).AsQueryable();
            #region Filter
            if (!string.IsNullOrEmpty(Filter.Title))
            {
                query = query.Where(d => EF.Functions.Like(d.Title, $"{Filter.Title}"));
            }
            #endregion
            #region Pagging
            var pagger = Pager.Build(Filter.PageId, await query.CountAsync());
            var AllEntities = await query.Pagging(pagger).ToListAsync();

            #endregion
            return Filter.SetData(AllEntities).SetPagging(pagger);
        }

        public async Task CreateColor(CreateColorDTO dTO)
        {
            var color = new ProductColor
            {
                ColorCode = dTO.ColorCode,
                Title = dTO.Title,
            };
            await _ProductColorRepositoiry.AddEntity(color);
            await _ProductColorRepositoiry.SaveAsync();
            ArgumentNullException.ThrowIfNull(dTO);
            if (string.IsNullOrWhiteSpace(dTO.Title) || string.IsNullOrWhiteSpace(dTO.ColorCode))
                throw new ArgumentException("Color title and color code are required.", nameof(dTO));

            await _ProductColorRepositoiry.Addentity(new ProductColor
            {
                Title = dTO.Title.Trim(),
                ColorCode = dTO.ColorCode.Trim()
            });
            await _ProductColorRepositoiry.SaveAsync();
        }

        public async Task<EditColorDTO> GetEditColor(long ColorId)
        {
            var color = await _ProductColorRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == ColorId && !item.isDelete);
            if (color == null)
                return null!;

            return new EditColorDTO
            {
                ColorId = color.id,
                Title = color.Title,
                ColorCode = color.ColorCode
            };
        }

        public async Task EditColor(EditColorDTO dTO)
        {
            ArgumentNullException.ThrowIfNull(dTO);
            var color = await _ProductColorRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.ColorId && !item.isDelete);
            if (color == null)
                throw new KeyNotFoundException("Color was not found.");
            if (string.IsNullOrWhiteSpace(dTO.Title) || string.IsNullOrWhiteSpace(dTO.ColorCode))
                throw new ArgumentException("Color title and color code are required.", nameof(dTO));

            color.Title = dTO.Title.Trim();
            color.ColorCode = dTO.ColorCode.Trim();
            _ProductColorRepositoiry.EditEntity(color);
            await _ProductColorRepositoiry.SaveAsync();
        }

        public async Task<bool> DeletColor(long ColorId)
        {
            var color = await _ProductColorRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == ColorId && !item.isDelete);
            if (color == null)
                return false;
            if (await _ProductVarientRepositoiry.GetQuery().AnyAsync(item => item.colorId == ColorId))
                return false;

            _ProductColorRepositoiry.DeleteEntity(color);
            await _ProductColorRepositoiry.SaveAsync();
            return true;
        }
        public async Task<List<ProductColor>> GetAllProductColor()
        {
            return await _ProductColorRepositoiry.GetQuery().ToListAsync();
        }
        #endregion

        #region Feture
        public async Task<bool> DeleteFeture(long fetureId)
        {
            var feature = await _ProductFetureRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == fetureId && !item.isDelete);
            if (feature == null)
                return false;
            _ProductFetureRepositoiry.DeleteEntity(feature);
            await _ProductFetureRepositoiry.SaveAsync();
            return true;
        }
        #endregion
        #region Gallery
        public async Task CreateGallery(CreateGalleryDTO dTO)
        {
            ArgumentNullException.ThrowIfNull(dTO);
            if (dTO.ImageName == null || dTO.ImageName.Length == 0)
                throw new ArgumentException("A gallery image is required.", nameof(dTO));
            var product = await _ProductRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.ProductId && !item.isDelete);
            if (product == null)
                throw new KeyNotFoundException("Product was not found.");

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var relativePath = await dTO.ImageName.UploadImageAsync(
                webRootPath, Path.Combine("images", "products"), maxWidth: 150, maxHeight: 150);
            await _ProductGalleryRepositoiry.Addentity(new ProductGallery
            {
                ProductId = product.id,
                ImageName = relativePath,
                Title = Path.GetFileNameWithoutExtension(relativePath),
                Order = dTO.Order
            });
            await _ProductGalleryRepositoiry.SaveAsync();
        }

        public async Task<EditGalleryDTO> GetEditGallery(long GalleryId)
        {
            var gallery = await _ProductGalleryRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == GalleryId && !item.isDelete);
            if (gallery == null)
                return null!;
            return new EditGalleryDTO { GalleryId = gallery.id, Order = gallery.Order };
        }

        public async Task EditGallery(EditGalleryDTO dTO)
        {
            ArgumentNullException.ThrowIfNull(dTO);
            var gallery = await _ProductGalleryRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.GalleryId && !item.isDelete);
            if (gallery == null)
                throw new KeyNotFoundException("Gallery image was not found.");
            gallery.Order = dTO.Order;
            _ProductGalleryRepositoiry.EditEntity(gallery);
            await _ProductGalleryRepositoiry.SaveAsync();
        }

        public async Task<bool> DeleteGallery(long GalleryId)
        {
            var gallery = await _ProductGalleryRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == GalleryId && !item.isDelete);
            if (gallery == null)
                return false;
            gallery.ImageName?.DeleteImage();
            _ProductGalleryRepositoiry.DeleteEntity(gallery);
            await _ProductGalleryRepositoiry.SaveAsync();
            return true;
        }
        #endregion
        #region ProductVarient
        public async Task CreateProductVarient(CreateProductVarientDTO dTO)
        {
            var varient = new ProductVarient
            {
                Price = dTO.Price,
                colorId = dTO.ColorId,
                ProductId = dTO.ProductId,
                stockcount = dTO.StockCount,
                Product = await _ProductRepositoiry.GetEntityById(dTO.ProductId),
                ProductColor = await _ProductColorRepositoiry.GetEntityById(dTO.ColorId)

            };
            await _ProductVarientRepositoiry.AddEntity(varient);
            await _ProductVarientRepositoiry.SaveAsync();
            ArgumentNullException.ThrowIfNull(dTO);
            var product = await _ProductRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.ProductId && !item.isDelete);
            var color = await _ProductColorRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.ColorId && !item.isDelete);
            if (product == null || color == null || dTO.Price < 0 || dTO.StockCount < 0)
                throw new ArgumentException("A valid product, color, nonnegative price, and stock count are required.", nameof(dTO));

            await _ProductVarientRepositoiry.Addentity(new ProductVarient
            {
                Product = product,
                ProductColor = color,
                colorId = color.id,
                Price = dTO.Price,
                stockcount = dTO.StockCount
            });
            await _ProductVarientRepositoiry.SaveAsync();
        }

        public async Task<EditProductVarientDTO> GetEditProductVarient(long VarientId)
        {
            var variant = await _ProductVarientRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == VarientId && !item.isDelete);
            if (variant == null)
                return null!;
            return new EditProductVarientDTO
            {
                VarientId = variant.id,
                ColorId = variant.colorId,
                Price = variant.Price,
                StockCount = variant.stockcount
            };
        }

        public async Task EditProductVarient(EditProductVarientDTO dTO)
        {
            ArgumentNullException.ThrowIfNull(dTO);
            var variant = await _ProductVarientRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.VarientId && !item.isDelete);
            var color = await _ProductColorRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == dTO.ColorId && !item.isDelete);
            if (variant == null || color == null || dTO.Price < 0 || dTO.StockCount < 0)
                throw new ArgumentException("A valid variant, color, nonnegative price, and stock count are required.", nameof(dTO));

            variant.ProductColor = color;
            variant.colorId = color.id;
            variant.Price = dTO.Price;
            variant.stockcount = dTO.StockCount;
            _ProductVarientRepositoiry.EditEntity(variant);
            await _ProductVarientRepositoiry.SaveAsync();
        }

        public async Task<bool> DeletProductVarient(long VarientId)
        {
            var variant = await _ProductVarientRepositoiry.GetQuery()
                .FirstOrDefaultAsync(item => item.id == VarientId && !item.isDelete);
            if (variant == null)
                return false;
            _ProductVarientRepositoiry.DeleteEntity(variant);
            await _ProductVarientRepositoiry.SaveAsync();
            return true;
        }

        public async Task<List<ProductCategory>> GetAllProductCategory()
        {
            return await _ProductSelectedRepository.GetQuery()
                .Include(category => category.ProductCategories)
                .Where(category => category.IsActive)
                .ToListAsync();
        }

        Task<string?> IProductService.ProductDetails(long productId)
        {
            throw new NotImplementedException();
        }




        #endregion
        #region Brand
        public async Task<FilterBrandDTO> FilterBrand(FilterBrandDTO Filter)
        {
            ArgumentNullException.ThrowIfNull(Filter);
            #region Query
            var query = _BrandRepository.GetQuery().OrderByDescending(d => d.CreatedTime).AsQueryable();

            #endregion

            #region Filter
            if (!string.IsNullOrEmpty(Filter.Title))
            {
                query = query.Where(d => EF.Functions.Like(d.Title, $"{Filter.Title}"));
            }
            if (!string.IsNullOrEmpty(Filter.URl))
            {
                query = query.Where(d => EF.Functions.Like(d.Url, $"{Filter.URl}"));
            }
            #endregion
            #region Pagging
            var pagger = Pager.Build(Filter.PageId, await query.CountAsync());
            var AllEntities = await query.Pagging(pagger).ToListAsync();

            #endregion
            return Filter.SetData(AllEntities).SetPagging(pagger);
        }

        public async Task<bool> CreateBrand(CreateBrandDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            if (dto.ImageName == null || dto.ImageName.Length == 0)
                return false;

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var data = new Brand
            {
                Title = dto.Title,
                ImageName = await dto.ImageName.UploadImageAsync(
                    webRootPath,
                    Path.Combine("images", "brands"),
                    maxWidth: 150,
                    maxHeight: 150),
                Url = dto.Url,
                Order = dto.Order
            };
            await _BrandRepository.Addentity(data);
            await _BrandRepository.SaveAsync();
            return true;

        }

        public async Task<bool> EditBrand(EditBrandDTO dto)
        {
            var data = await _BrandRepository.GetEntityById(dto.BrandId);
            data.Order = dto.Order;
            data.Title = dto.Title;
            data.Url = dto.Url;

            if (dto.ImageName == null)
            {
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            _BrandRepository.EditEntity(data);
            await _BrandRepository.SaveAsync();
            return true;        }

        public async Task<EditBrandDTO> getEditBrand(long BrandId)
        {
            var data = await _BrandRepository.GetEntityById(BrandId);
            return new EditBrandDTO
            {
                Order = data.Order,
                Title = data.Title,
                Url = data.Url,
                BrandId = data.id,


            };

        
        }

        public async Task<bool> DeleteBrand(long BrandId)
        {
            var inUse = await _ProductSelectBrandRepository.GetQuery().AnyAsync(d => d.BrandId == BrandId);
            if (inUse) return false;

            var data = await _BrandRepository.GetEntityById(BrandId);
            _BrandRepository.DeleteEntity(data);
            await _BrandRepository.SaveAsync();
            return true;

        }

        public async Task<List<Brand>> GetAllBrand()
        {
            return await _BrandRepository.GetQuery().ToListAsync();
        }

        public Task<string?> EditCategory(long categoryId)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
