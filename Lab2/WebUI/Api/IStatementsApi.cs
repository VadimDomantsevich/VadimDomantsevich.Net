using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IStatementsApi
    {
        [Get("Statements")]
        Task<List<Statement>> GetAll();

        [Get("Statements/{id}")]
        Task<Statement> GetById([Path] int id);

        [Post("Statements")]
        Task Add([Body] Statement Statement);

        [Put("Statements")]
        Task Update([Body] Statement Statement);

        [Delete("Statements")]
        Task Delete(int id);

        [Get("Statements/GetStatementsByStudentId/{id}")]
        Task<List<Statement>> GetStatementsByStudentId([Path] int id);

        [Get("Statements/GetStatementsBySubjectId/{id}")]
        Task<List<Statement>> GetStatementsBySubjectId([Path] int id);

        [Get("Statements/GetStatementsBySemesterId/{id}")]
        Task<List<Statement>> GetStatementsBySemesterId([Path] int id);
    }
}
