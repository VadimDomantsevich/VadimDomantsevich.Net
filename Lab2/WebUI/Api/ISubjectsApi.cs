using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface ISubjectsApi
    {
        [Get("Subjects")]
        Task<List<Subject>> GetAll();

        [Get("Subjects/{id}")]
        Task<Subject> GetById([Path] int id);

        [Post("Subjects")]
        Task Add([Body] Subject subject);

        [Put("Subjects")]
        Task Update([Body] Subject subject);

        [Delete("Subjects")]
        Task Delete(int id);

        [Get("Subjects/GetSubjectByName")]
        Task<Subject> GetSubjectByName(string name);
    }
}
