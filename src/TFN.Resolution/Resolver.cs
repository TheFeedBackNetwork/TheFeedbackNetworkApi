using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using TFN.Api.Authorization.Handlers;
using TFN.Domain.Interfaces.Components;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services;
using TFN.Domain.Services.Validators;
using TFN.Infrastructure.Components.Storage;
using TFN.Infrastructure.Repositories.ClientAggregate.InMemory;
using TFN.Infrastructure.Repositories.PostAggregate.InMemory;
using TFN.Infrastructure.Repositories.ScopeAggregate.InMemory;
using TFN.Infrastructure.Repositories.TrackAggregate.InMemory;
using TFN.Infrastructure.Repositories.UserAggregate.InMemory;
using TFN.Mvc.Extensions;

namespace TFN.Resolution
{
    public static class Resolver
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            //repositories
            services.AddTransient<IScopeStore, ScopeInMemoryRepository>();
            services.AddTransient<IUserRepository, UserInMemoryRepository>();
            services.AddTransient<IClientRepository, ClientInMemoryRepository>();
            services.AddTransient<IPostRepository, PostInMemoryRepository>();
            services.AddTransient<IClientStore, ClientInMemoryRepository>();
            services.AddTransient<ITrackRepository, TrackInMemoryRepository>();
            //services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICorsPolicyService, CorsPolicyService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<ITrackProcessingService, TrackProcessingService>();
            services.AddTransient<ITrackStorageService, TrackStorageService>();
            //validators
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            //components
            services.AddTransient<IS3StorageComponent, S3StorageComponent>();
            services.AddTransient<IBlobStorageComponent, BlobStorageComponent>();
        }

        public static void RegisterAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddScopePolicy("posts.read")
                    .AddScopePolicy("posts.write")
                    .AddScopePolicy("posts.edit")
                    .AddScopePolicy("posts.delete")
                    .AddScopePolicy("tracks.read")
                    .AddScopePolicy("tracks.write")
                    .AddScopePolicy("tracks.delete");
            });

            services.AddTransient<IAuthorizationHandler, PostAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, CommentAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, ScoreAuthorizationHandler>();
        }
    }
}
