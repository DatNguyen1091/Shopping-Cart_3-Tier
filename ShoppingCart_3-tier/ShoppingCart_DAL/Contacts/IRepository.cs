
namespace ShoppingCart_DAL.Contacts
{
    public interface IRepository<T>
    {
        public List<T> GetAll(int? page);
        public T GetById(int id);
        public T Create(T model);
        public T Update(int id, T model);
        public string Delete(int id);
    }
}
