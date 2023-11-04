namespace Core
{
    public class Pagination
    {
        public Pagination(int pageSize, int pageNumber, int total)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageNumber;
            this.Total = total;

        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Total { get; set; }
    }
}