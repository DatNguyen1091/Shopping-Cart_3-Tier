namespace ShoppingCart_DAL.Models
{
    public class Categories : Common
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }
        public string? description { get; set; }
        public string? metaDescription { get; set; }
        public string? metaKeywords { get; set; }
        public string? categoryStatus { get; set; }
        public bool isDeleted { get; set; }
    }
}
