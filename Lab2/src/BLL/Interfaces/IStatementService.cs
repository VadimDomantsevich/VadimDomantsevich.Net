using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStatementService : IService<Statement>
    {
        Task<IEnumerable<Statement>> GetStatementsByStudentId(int studentId);

        Task<IEnumerable<Statement>> GetStatementsBySubjectId(int subjectId);

        Task<IEnumerable<Statement>> GetStatementsBySemesterId(int semesterId);
    }
}
