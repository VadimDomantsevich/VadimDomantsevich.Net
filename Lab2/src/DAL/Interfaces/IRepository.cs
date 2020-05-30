using System.Linq;

namespace DAL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();

        void Create(T item);

        void Update(T item);

        void Delete(T item);

        T GetById(int id);
    }
}
