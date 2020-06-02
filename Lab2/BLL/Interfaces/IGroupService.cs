using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGroupService : IService<Group>
    {
        Task<IEnumerable<Group>> GetGroupsBySpecialtyId(int specialtyId);

        Task<Group> GetGroupByName(string name);
    }
}
