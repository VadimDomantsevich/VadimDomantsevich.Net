using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.EntityFramework
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DBContext _dBContext;

        public Repository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public void Create(T item)
        {
            _dBContext.Set<T>().Add(item);
            _dBContext.SaveChanges();
        }

        public void Delete(T item)
        {
            _dBContext.Set<T>().Remove(item);
            _dBContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            var items = _dBContext.Set<T>().AsNoTracking();
            return items;
        }

        public T GetById(int id)
        {
            var item = _dBContext.Set<T>().Find(id);

            _dBContext.Entry(item).State = EntityState.Detached;

            return item;
        }

        public void Update(T item)
        {
            _dBContext.Entry(item).State = EntityState.Modified;

            _dBContext.SaveChanges();
        }
    }
}
