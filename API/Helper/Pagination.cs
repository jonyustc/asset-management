namespace API.Helper
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageSize, int pageIndex, int total,IReadOnlyList<T> data)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Total = total;
            this.Data = data;

        }
        public int PageSize { get; set; } = 50;
        public int PageIndex { get; set; } = 1;
        public int Total { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}