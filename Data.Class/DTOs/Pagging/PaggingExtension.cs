namespace Data.Class.DTOs.Pagging
{
    public static class PaggingExtension
    {
        /// <summary>Applies the requested page to an IQueryable.</summary>
        public static IQueryable<T> Pagging<T>(this IQueryable<T> query, BasePagging paging)
        {
            ArgumentNullException.ThrowIfNull(query);
            ArgumentNullException.ThrowIfNull(paging);
            return query.Skip(paging.SkipEntity).Take(paging.TakeEntity);
        }

        /// <summary>Spelling-correct alias for Pagging.</summary>
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, BasePagging paging) =>
            query.Pagging(paging);

        /// <summary>Builds page metadata from the total number of matching records.</summary>
        public static Pager ToPager(this BasePagging paging, int totalCount)
        {
            ArgumentNullException.ThrowIfNull(paging);
            ArgumentOutOfRangeException.ThrowIfNegative(totalCount);
            return new Pager(paging.PageNumber, paging.PageSize, totalCount);
        }
    }
}
