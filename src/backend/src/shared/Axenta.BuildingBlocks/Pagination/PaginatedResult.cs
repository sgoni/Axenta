namespace Axenta.BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public int Count { get; set; } = count;
    public IEnumerable<TEntity> Data { get; set; } = data;
}