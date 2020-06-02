using BLL.Models;

namespace BLL.Interfaces
{
    interface ISubjectServices : IServices<Subject>
    {
        Subject GetSubjectByName(string name);
    }
}
