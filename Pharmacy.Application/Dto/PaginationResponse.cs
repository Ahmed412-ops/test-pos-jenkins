using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Helper.Extensions;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Dto;

public class PaginationResponse<T>
{
    public int Count { get; set; }
    public IEnumerable<T> Data { get; set; } = [];
    
    /// <summary>
    /// Creates a PaginationResponse by executing count and data queries in parallel
    /// </summary>
    public static async Task<PaginationResponse<T>> CreateAsync<TSource>(
        IQueryable<TSource> query,
        Expression<Func<TSource, T>> selector,
        Pagination request,
        CancellationToken cancellationToken = default)
    {
        var countTask = query.CountAsync(cancellationToken);
        var dataTask = query
            .Select(selector)
            .Paginate(request)
            .ToListAsync(cancellationToken);

        await Task.WhenAll(countTask, dataTask);

        return new PaginationResponse<T>
        {
            Count = await countTask,
            Data = (IEnumerable<T>)await dataTask
        };
    }

    /// <summary>
    /// Creates a PaginationResponse when the data is already of the target type
    /// </summary>
    public static async Task<PaginationResponse<T>> CreateAsync(
        IQueryable<T> query,
        Pagination request,
        CancellationToken cancellationToken = default)
    {
        return await CreateAsync(query, x => x, request, cancellationToken);
    }
}
