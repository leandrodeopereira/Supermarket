#nullable disable
namespace SupermarketApi.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string picturePath)
        {
            this.ProductItemId = productItemId;
            this.ProductName = productName;
            this.PicturePath = picturePath;
        }

        public string PicturePath { get; set; }

        public int ProductItemId { get; set; }

        public string ProductName { get; set; }
    }
}
