using System.Collections.Generic;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStatementServices : IServices<Statement>
    {
        IEnumerable<Statement> GetStatementsByStudentId(int studentId);

        IEnumerable<Statement> GetStatementsBySubjectId(int subjectId);

        IEnumerable<Statement> GetStatementsBySemesterId(int semesterId);
    }
}
