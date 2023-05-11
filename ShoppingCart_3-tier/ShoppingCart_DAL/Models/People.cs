namespace ShoppingCart_DAL.Models
{
    public class People : Common
    {
        public int id { get; set; }
        public string? firstName { get; set; }
        public string? middleName { get; set; }
        public string? lastName { get; set; }
        public string? emailAddress { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isDeleted { get; set; }
    }
}
