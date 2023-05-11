namespace ShoppingCart_DAL.Models
{
    public class ProductCategories : Common
    {
        public int id { get; set; }
        public int productId { get; set; }
        public int categoryId { get; set; }
    }
}
