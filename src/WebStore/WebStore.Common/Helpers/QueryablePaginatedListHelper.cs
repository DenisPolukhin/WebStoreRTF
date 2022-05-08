using Microsoft.EntityFrameworkCore;
using WebStore.Common.Models;

namespace WebStore.Common.Helpers;

public static class QueryablePaginatedListHelper
{
    public static async Task<IPaginatedList<T>> ToPaginatedList<T>(this IQueryable<T> source, int pageIndex,
        int pageSize)
    {
        if (pageIndex < 1)
            throw new ArgumentException("Page index start from 1");

        var count = await source.CountAsync();

        var data = await source.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalCount = count,
            TotalPages = (int) Math.Ceiling(count / (double) pageSize),
            Data = data
        };
    }
}