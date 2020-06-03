using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAll();

        Task Create(T item);

        Task Update(T item);

        Task Delete(T item);

        Task<T> GetById(int id);
    }
}
