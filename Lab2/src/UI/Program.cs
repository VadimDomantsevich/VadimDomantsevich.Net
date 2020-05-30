using BLL.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using UI.View.MainConsoleService;

namespace UI
{
    internal class Program
    {
        private const string _configFileName = "appsettings.json";

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(_configFileName);
            var config = builder.Build();

            var services = DIConfigurator.Configure(config);

            var mainMenu = services.GetService<MainMenuBaseConsoleService>();
            mainMenu.StartLoop();
        }
    }
}
