using AutoMapper;
using BLL.Infrastructure;
using DAL.EntityFramework;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebUI.DI
{
    public static class DIConfigurator
    {
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration config)
        {
            var businessAssembly = Assembly.Load("BLL");

            services.AddDbContext<DBContext>(options => options.UseSqlServer(config.GetConnectionString("local")))
                    .Scan(scan => scan.FromAssemblies(businessAssembly)
                                      .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                                      .AsSelf()
                                      .WithScopedLifetime())
                    .AddAutoMapper(typeof(MapperProfile))
                    .AddScoped(typeof(IRepository<>), typeof(GeneralRepository<>));
        }
    }
}
