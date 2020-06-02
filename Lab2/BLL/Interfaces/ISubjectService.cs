using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface ISubjectService : IService<Subject>
    {
        Task<Subject> GetSubjectByName(string name);
    }
}
