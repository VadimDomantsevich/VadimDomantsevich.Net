using BLL.Models;

namespace BLL.Interfaces
{
    interface ISpecialtyServices : IServices<Specialty>
    {
        Specialty GetSpecialtyByName(string name);
    }
}
