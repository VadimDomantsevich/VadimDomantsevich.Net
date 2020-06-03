using BLL.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Api
{
    public interface IGroupsApi
    {
        [Get("Groups")]
        Task<List<Group>> GetAll();

        [Get("Groups/{id}")]
        Task<Group> GetById([Path] int id);

        [Post("Groups")]
        Task Add ([Body] Group item);

        [Delete("Groups")]
        Task Delete(int id);

        [Get("Groups/GetGroupsBySpecialtyId")]
        Task<List<Student>> GetGroupsBySpecialtyId(int specialtyId);

        [Get("Groups/GetGroupByName")]
        Task<List<Student>> GetGroupByName(string name);

        [Put("Groups")]
        Task Update(Group item);
    }
}
