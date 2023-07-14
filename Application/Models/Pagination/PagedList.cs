namespace Application.Models.Pagination;
public class PagedList<T>
{
    public PagedList(IQueryable<T> query, int pageIndex, int pageSize)
    {
        var totalCount = query.Count();
        int recordsToSkip = (pageIndex - 1) * pageSize;

        var data = query.Skip(recordsToSkip)
                        .Take(pageSize)
                        .ToList();

        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        TotalRecords = totalCount;
        Data = data;
    }

    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public List<T> Data { get; set; }
}
