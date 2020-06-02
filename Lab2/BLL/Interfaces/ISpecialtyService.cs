using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface ISpecialtyService : IService<Specialty>
    {
        Task<Specialty> GetSpecialtyByName(string name);
    }
}
