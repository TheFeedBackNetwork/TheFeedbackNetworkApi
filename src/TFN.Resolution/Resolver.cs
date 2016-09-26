using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services;
using TFN.Domain.Services.Validators;
using TFN.Infrastructure.Repositories.ClientAggregate.InMemory;
using TFN.Infrastructure.Repositories.PostAggregate.InMemory;
using TFN.Infrastructure.Repositories.ScopeAggregate.InMemory;
using TFN.Infrastructure.Repositories.UserAggregate.InMemory;
using TFN.Mvc.Extensions;

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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
        }

        public static void RegisterAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddScopePolicy("post.read")
                    .AddScopePolicy("post.write")
                    .AddScopePolicy("posts.edit")
                    .AddScopePolicy("posts.delete");
            });
        }
    }
}
