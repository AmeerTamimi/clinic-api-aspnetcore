namespace ClinicAPI.Responses
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public int Page { get; set; }
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;

        private PagedResult() { }

        public static PagedResult<T> GetPagedItems(IEnumerable<T> items, int totalItems, int page, int pageSize)
        {
            return new PagedResult<T>
            {
                Items = items,
                TotalItems = totalItems,
                PageSize = pageSize,
                Page = page
            };
        }
    }
}
