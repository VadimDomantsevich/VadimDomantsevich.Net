using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using UI.DI;
using UI.Services.MainConsoleService;

namespace UI
{
    internal class Program
    {
        private const string _configFileName = "appsettings.json";

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(_configFileName);
            var config = builder.Build();

            var services = DIConfigurator.Configure(config);

            var mainMenu = services.GetService<MainMenuBaseConsoleService>();
            await mainMenu.StartLoop();
        }
    }
}
