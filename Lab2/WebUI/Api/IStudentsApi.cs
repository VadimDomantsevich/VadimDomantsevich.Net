using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IStudentsApi
    {
        [Get("Students")]
        Task<List<Student>> GetAll();

        [Get("Students/{id}")]
        Task<Student> GetById([Path] int id);

        [Post("Students")]
        Task Add ([Body] Student student);

        [Put("Students")]
        Task Update([Body] Student student);

        [Delete("Students")]
        Task Delete(int id);

        [Get("Students/GetStudentByRecordNumber")]
        Task<Student> GetStudentByRecordNumber(string number);

        [Get("Students/GetStudentByPhoneNumber")]
        Task<Student> GetStudentByPhoneNumber(string number);
    }
}
