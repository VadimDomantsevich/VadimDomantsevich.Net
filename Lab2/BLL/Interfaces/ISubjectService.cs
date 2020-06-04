using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISubjectService : IService<Subject>
    {
        Task<Subject> GetSubjectByName(string name);
    }
}
