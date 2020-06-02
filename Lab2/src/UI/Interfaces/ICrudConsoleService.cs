using System.Threading.Tasks;

namespace UI.Interfaces
{
    public interface ICrudConsoleService<T>
    {
        Task Create();

        Task Delete();

        Task Update();

        Task<T> CreateModel();
    }
}
