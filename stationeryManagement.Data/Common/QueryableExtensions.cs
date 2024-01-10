using Microsoft.EntityFrameworkCore;

namespace MangSeal.Data.Common;

public static class QueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T>? source, int page = 1, int pageSize = 20)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 20 : (pageSize > 50 ? 50 : pageSize);

        var pagedList = new PagedList<T>
        {
            PageIndex = page,
            PageSize = pageSize,
            TotalItemCount = source == null ? 0 : await source.CountAsync(),
        };

        if (source != null && pagedList.TotalItemCount > 0)
        {
            pagedList.Items.AddRange(source.Skip((page - 1) * pageSize).Take(pageSize));
        }
        return pagedList;
    }

    public static PagedList<T> ToPagedListByOffset<T>(this ICollection<T> source, int offset, int limit = 30)
    {
        if (limit < 0)
        {
            limit = 50;
        }

        if (limit == 0)
        {
            offset = 0;
            limit = source.Count();
        }

        var pagedList = new PagedList<T>
        {
            PageSize = limit,
            TotalItemCount = source?.Count() ?? 0,
            PageIndex = limit > 0 ? offset / limit + 1 : 1
        };

        if (source != null && pagedList.TotalItemCount > 0)
        {
            pagedList.Items.AddRange(source.Skip(offset).Take(limit));
        }

        return pagedList;
    }
}