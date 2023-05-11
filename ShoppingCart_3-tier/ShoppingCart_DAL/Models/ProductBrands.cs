namespace ShoppingCart_DAL.Models
{
    public class ProductBrands : Common
    {
        public int id { get; set; }
        public int productId { get; set; }
        public int brandId { get; set; }
    }
}
