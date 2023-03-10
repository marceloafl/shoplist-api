namespace ShoplistAPI.Pagination
{
    public class ProductParameters
    {
        const int maxSize = 50;
        public int Page { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxSize) ? maxSize : value;
            }
        }
    }
}
