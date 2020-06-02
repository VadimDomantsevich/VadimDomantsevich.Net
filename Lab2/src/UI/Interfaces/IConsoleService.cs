using System.Threading.Tasks;

namespace UI.Interfaces
{
    public interface IConsoleService
    {
        void PrintMenu();

        Task StartLoop();
    }
}
