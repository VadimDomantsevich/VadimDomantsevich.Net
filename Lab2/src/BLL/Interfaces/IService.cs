using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<T>
    {
        Task Create(T item);

        Task Delete(int id);

        Task Update(T item);

        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();
    }
}
