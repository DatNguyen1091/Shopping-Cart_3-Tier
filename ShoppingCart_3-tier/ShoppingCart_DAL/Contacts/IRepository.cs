using ShoppingCart_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingCart_DAL.Data;


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
