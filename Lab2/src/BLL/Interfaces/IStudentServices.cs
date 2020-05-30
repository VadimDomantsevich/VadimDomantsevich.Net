using System.Collections.Generic;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStudentServices : IServices<Student>
    {
        IEnumerable<Student> GetStudentsByGroupId(int groupId);

        Student GetStudentByRecordNumber(string recordNumber);

        Student GetStudentByPhoneNumber(string phoneNumber);
    }
}
