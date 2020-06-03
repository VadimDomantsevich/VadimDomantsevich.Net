using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISpecialtyService : IService<Specialty>
    {
        Task<Specialty> GetSpecialtyByName(string name);
    }
}
