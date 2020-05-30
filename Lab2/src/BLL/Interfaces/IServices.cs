using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IServices<T>
    {
        void Create(T item);

        void Delete(int id);

        void Update(T item);

        T GetById(int id);

        IEnumerable<T> GetAll();
    }
}
