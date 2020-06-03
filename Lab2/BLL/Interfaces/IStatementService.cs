using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStatementService : IService<Statement>
    {
        Task<List<Statement>> GetStatementsByStudentId(int studentId);

        Task<List<Statement>> GetStatementsBySubjectId(int subjectId);

        Task<List<Statement>> GetStatementsBySemesterId(int semesterId);
    }
}
