
namespace ShoppingCart_DAL.Models
{
    public class Users : Common
    {
        public int id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

    }
}
