using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services;
using TFN.Infrastructure.Repositories.ClientAggregate.InMemory;
using TFN.Infrastructure.Repositories.PostAggregate.InMemory;
using TFN.Infrastructure.Repositories.ScopeAggregate.InMemory;
using TFN.Infrastructure.Repositories.UserAggregate.InMemory;

namespace TFN.Resolution
{
    public static class Resolver
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IUserRepository, UserInMemoryRepository>();
            services.AddTransient<IClientRepository, ClientInMemoryRepository>();
            services.AddTransient<IPostRepository, PostInMemoryRepository>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IClientStore, ClientInMemoryRepository>();
            services.AddTransient<ICorsPolicyService, CorsPolicyService>();
            services.AddTransient<IScopeStore, ScopeInMemoryRepository>();
        }

        public static void RegisterAuthorizationPolicies(IServiceCollection services)
        {
        }
    }
}
