using AutoMapper;
using BLL.Infrastructure;
using DAL.EntityFramework;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BLL.DI
{
    public static class DIConfigurator
    {
        public static ServiceProvider Configure(IConfigurationRoot config)
        {
            var businessAssembly = Assembly.Load("BLL");
            var consoleAssembly = Assembly.Load("UI");

            return new ServiceCollection()
                .AddDbContext<DBContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("local")), ServiceLifetime.Transient)
                .Scan(scan => scan
                    .FromAssemblies(businessAssembly, consoleAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Services")))
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("BaseConsoleService")))
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("PrintConsoleService")))
                    .AsSelf()
                    .WithTransientLifetime())
                .Scan(scan => scan
                    .FromAssemblies(businessAssembly, consoleAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("CrudConsoleService")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime())
                .AddSingleton(config)
                .AddAutoMapper(typeof(MapperProfile))
                .AddTransient(typeof(IRepository<>), typeof(Repository<>))
                .BuildServiceProvider();
        }
    }
}
