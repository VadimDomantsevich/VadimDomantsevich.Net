using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface ISpecialtiesApi
    {
        [Get("Specialties")]
        Task<List<Specialty>> GetAll();

        [Get("Specialties/{id}")]
        Task<Specialty> GetById([Path] int id);

        [Post("Specialties")]
        Task Add([Body] Specialty specialty);

        [Put("Specialties")]
        Task Update([Body] Specialty specialty);

        [Delete("Specialty")]
        Task Delete(int id);

        [Get("Specialties/GetSpecialtyByName")]
        Task<Group> GetSpecialtyByName(string name);
    }
}
