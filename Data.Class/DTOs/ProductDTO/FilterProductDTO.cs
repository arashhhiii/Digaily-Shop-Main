using Data.Class.DTOs.Pagging;
using Data.Class.entities.ProductEntities;
using System.ComponentModel.DataAnnotations;

namespace Data.Class.DTOs.ProductDTO
{
    public class FilterProductDTO : Pager
    {
        public string title {  get; set; }
        public long? BrandId { get; set; }
        public long? categoryId { get; set; }
        public int? MostExpensiveProice {  get; set; }
        public int? MinExpensivePrice {  get; set; }
        public int? StartPrice {  get; set; }
        public int? endPrice { get; set; }
       public long? ColorId {  get; set; }
 public ProductCategory Category { get; set; }
        public FilterProductStatus ProductStatus{  get; set; }
        public FilterProductOrder ProductOrder{  get; set; }
        public List<Product> Data {  get; set; }
     
        #region Methods
        public FilterProductDTO SetData(List<Product> data)
        {
            Data = data;
            return this;

        }

        public FilterProductDTO SetPagging(BasePagging Paging)
        {
            PageId = Paging.Id;
            AllEntitiesCount = Paging.AllEntitiesCount;
            SatrtPage = Paging.StartPage;
            EndPage = Paging.EndPage;
            HowManyShopPageAfterAndBefore = Paging.HowManyShopPageAfterAndBefore;
            TakeEntity = Paging.TakeEntity;
            SkipEntity = Paging.SkipEntity;
            PageCount = Paging.PageCount;
            return this;
        }
        #endregion
    }
    public enum FilterProductStatus
    {
        [Display(Name ="All")]
        All,
        [Display(Name = "Active")]
        Active,
        [Display(Name = "DiActive")]
        DiActive,

        [Display(Name = "Available")]
        Available,

        [Display(Name = "NotAvailable")]
        NotAvailble
        

    }
    public enum FilterProductOrder
    {
        [Display(Name = "Newest")]
        Newest,
        [Display(Name = "Oldest")]
        Oldest,
        [Display(Name = "Expensive")]
        Expensive,

        [Display(Name = "Cheap")]
        Cheap

    }
}
