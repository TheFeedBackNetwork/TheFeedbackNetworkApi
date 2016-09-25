using Microsoft.Extensions.DependencyInjection;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services;
using TFN.Infrastructure.Repositories.PostAggregate.InMemory;
using TFN.Infrastructure.Repositories.UserAggregate.InMemory;

namespace TFN.Domain.Resolution
{
    public class Resolver
    {
        public void RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IUserRepository, UserInMemoryRepository>();
            services.AddTransient<IPostRepository, PostInMemoryRepository>();
        }

        public void RegisterAuthorizationPolicies(IServiceCollection services)
        {
        }
    }
}
