using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebUI.Identity
{
    public class DBInitializer
    {
        private readonly IIdentityService _identityService;

        public DBInitializer(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task Seed()
        {
            var password = "123456Aa+";

            var users = new User[]
            {
                new User
                {
                    UserName = "Perdaker",
                    FirstName = "Vadim",
                    LastName = "Domantsevich",
                    Email = "perdaker@mail.ru",
                },

                new User
                {
                    UserName = "Seoriys",
                    FirstName = "Vadia",
                    LastName = "Domantsevich",
                    Email = "manager@mail.ru",
                },
            };

            var roles = new string[]
            {
                Roles.User,
                Roles.Manager,
            };

            foreach (var role in roles)
            {
                if (!await _identityService.RoleExistsAsync(role))
                {
                    await _identityService.CreateRoleAsync(new IdentityRole(role));
                }
            }

            var i = 0;
            foreach (var user in users)
            {
                if (await _identityService.FindByEmailAsync(user.Email) == null)
                {
                    await _identityService.RegisterAsync(user, password);
                    await _identityService.AddToRoleAsync(user, roles[i++]);
                }
            }
        }
    }
}
