namespace Data.Class.DTOs.Pagging
{
    public class Pager
    {
        public Pager() { }

        public Pager(int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = Math.Max(1, currentPage);
            PageSize = Math.Clamp(pageSize, 1, BasePagging.MaximumPageSize);
            TotalCount = Math.Max(0, totalCount);
            TotalPages = TotalCount == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
            if (TotalPages > 0 && CurrentPage > TotalPages)
                CurrentPage = TotalPages;
        }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = BasePagging.DefaultPageSize;
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public object HowManyShopPageAfterAndBefore { get;   set; }
        public int TakeEntity { get;   set; }
        public object PageId { get;   set; }
        public object AllEntitiesCount { get;   set; }
        public object SatrtPage { get;   set; }
        public object EndPage { get;   set; }
        public int SkipEntity { get;   set; }
        public object PageCount { get;   set; }

        public static BasePagging Build(object pageId, int totalCount)
        {
            var requestedPage = int.TryParse(pageId?.ToString(), out var parsedPage) ? parsedPage : 1;
            var result = new Pager(requestedPage, BasePagging.DefaultPageSize, totalCount);
            var pageNumber = result.TotalPages == 0 ? 1 : result.CurrentPage;
            const int pageRange = 2;

            return new BasePagging
            {
                PageNumber = pageNumber,
                PageSize = result.PageSize,
                Id = pageNumber,
                AllEntitiesCount = result.TotalCount,
                PageCount = result.TotalPages,
                StartPage = Math.Max(1, pageNumber - pageRange),
                EndPage = Math.Min(result.TotalPages, pageNumber + pageRange),
                HowManyShopPageAfterAndBefore = pageRange
            };
        }
    }
}
