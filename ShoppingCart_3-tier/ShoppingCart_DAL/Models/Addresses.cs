
namespace ShoppingCart_DAL.Models
{
    public class Addresses : Common
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? addressLine1 { get; set; }
        public string? addressLine2 { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public string? zipCode { get; set; }
        public string? addressType { get; set; }
        public bool isDeleted { get; set; }

    }
}
