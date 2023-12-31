namespace Core
{
    public class PaginationParams
    {
          private const int MaxPageSize = 50;
        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int? SellerId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int FyId { get; set; }
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                _search = value.ToLower();
            }
        }
    }
}