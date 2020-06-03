using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface ISemestersApi
    {
        [Get("Semesters")]
        Task<List<Semester>> GetAll();

        [Get("Semesters/{id}")]
        Task<Semester> GetById([Path] int id);

        [Post("Semesters")]
        Task Add([Body] Semester lecturer);

        [Put("Semesters")]
        Task Update([Body] Semester lecturer);

        [Delete("Semesters")]
        Task Delete(int id);
    }
}
