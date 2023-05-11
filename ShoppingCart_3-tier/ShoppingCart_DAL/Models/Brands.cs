namespace ShoppingCart_DAL.Models
{
    public class Brands : Common
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }
        public string? description { get; set; }
        public string? metaDescription { get; set; }
        public string? metaKeywords { get; set; }
        public string? brandStatus { get; set; }
        public bool isDelete { get; set; }
    }
}
