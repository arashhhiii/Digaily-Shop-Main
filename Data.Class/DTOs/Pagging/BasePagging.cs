namespace Data.Class.DTOs.Pagging
{
    /// <summary>Common request settings for one-based paged queries.</summary>
    public class BasePagging
    {
        public const int DefaultPageSize = 10;
        public const int MaximumPageSize = 100;

        private int _pageNumber = 1;
        private int _pageSize = DefaultPageSize;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = Math.Max(1, value);
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value, 1, MaximumPageSize);
        }

        public int SkipEntity => checked((PageNumber - 1) * PageSize);
        public int TakeEntity => PageSize;

        public object PageCount { get; internal set; }
        public object EndPage { get; internal set; }
        public object StartPage { get; internal set; }
        public object AllEntitiesCount { get; internal set; }
        public object Id { get; internal set; }
        public object HowManyShopPageAfterAndBefore { get; internal set; }
    }
}
