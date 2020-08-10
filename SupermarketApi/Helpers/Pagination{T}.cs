namespace SupermarketApi.Helpers
{
    using System.Collections.Generic;

    public class Pagination<T>
        where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyCollection<T> data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int Count { get; private set; }

        public IReadOnlyCollection<T> Data { get; private set; }
    }
}
