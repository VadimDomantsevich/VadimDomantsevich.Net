using BLL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IGroupServices : IServices<Group>
    {
        IEnumerable<Group> GetGroupsBySpecialtyId(int specialtyId);

        Group GetGroupByName(string name);
    }
}
