using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStudentService : IService<Student>
    {
        Task<List<Student>> GetStudentsByGroupId(int groupId);

        Task<Student> GetStudentByRecordNumber(string recordNumber);

        Task<Student> GetStudentByPhoneNumber(string phoneNumber);
    }
}
