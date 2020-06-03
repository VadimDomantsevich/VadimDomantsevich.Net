using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DTO;
using DAL.EntityFramework;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebUI.Identity;

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

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IIdentityService))
                    .AddClasses(classes => classes.AssignableTo(typeof(IIdentityService)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.AddSingleton<DBInitializer>();

            services.AddIdentity<UserDTO, IdentityRole>()
              .AddEntityFrameworkStores<DBContext>()
              .AddDefaultTokenProviders();
        }
    }
}
