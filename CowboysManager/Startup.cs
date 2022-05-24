using CowboysManager.Core.Entities;
using CowboysManager.Core.Interfaces;
using CowboysManager.Core.Services;
using CowboysManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CowboysManager
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services.AddSingleton<CowboysManager, CowboysManager>()
                .BuildServiceProvider()
                .GetService<CowboysManager>();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository<User>, UserRepository>();
            services.AddSingleton<IPlatformService, PlatformService>();
            services.AddSingleton<IPlatformRepository<Platform>, PlatformRepository>();

        }
    }
}