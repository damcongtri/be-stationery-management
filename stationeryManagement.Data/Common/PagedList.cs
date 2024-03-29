namespace MangSeal.Data.Common;

public class PagedList
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalItemCount { get; set; }
    public int PageCount => TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < PageCount;
    public bool IsFirstPage => PageIndex == 1;
    public bool IsLastPage => PageIndex >= PageCount;
    public int FirstItemOnPage => (PageIndex - 1) * PageSize + 1;
    public int LastItemOnPage => (FirstItemOnPage + PageSize - 1) > TotalItemCount ? TotalItemCount : FirstItemOnPage + PageSize - 1;
}

public class PagedList<T> : PagedList
{
    public List<T> Items { get; set; } = new List<T>();
}