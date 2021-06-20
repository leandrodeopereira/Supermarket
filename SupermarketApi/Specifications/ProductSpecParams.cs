namespace SupermarketApi.Specifications
{

    public sealed class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        private int pageSize = 6;
        private string? search;

        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => this.pageSize;
            set => this.pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string? Search
        {
            get => this.search;
            set => this.search = value?.ToLower();
        }

        public string? Sort { get; set; }
    }
}
