namespace Core
{
    public class Result<T>
    {
        public Result(IReadOnlyList<T> values, Pagination pagination, bool isSuccess)
        {
            Values = values;
            Pagination = pagination;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }
        public IReadOnlyList<T> Values { get; set; }
        public Pagination Pagination { get; set; }


        public static Result<T> CreatePagination(IQueryable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var pagination = new Pagination(pageSize, pageNumber, count);
            return new Result<T>(items, pagination, true);
        }
    }
}