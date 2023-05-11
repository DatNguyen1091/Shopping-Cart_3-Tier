namespace ShoppingCart_DAL.Models
{
    public class Products : Common
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }
        public string? description { get; set; }
        public string? metaDescription { get; set; }
        public string? metaKeywords { get; set; }
        public string? sku { get; set; }
        public string? model { get; set; }
        public decimal price { get; set; }
        public decimal oldPrice { get; set; }
        public string? imageUrl { get; set; }
        public bool isBestseller { get; set; }
        public bool isFeatured { get; set; }
        public int quantity { get; set; }
        public string? productStatus { get; set; }
        public bool isDeleted { get; set; }
    }
}
