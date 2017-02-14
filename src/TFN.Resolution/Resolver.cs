using System;
using System.Net;
using System.Net.Mail;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TFN.Api.Authorization.Handlers;
using TFN.Domain.Interfaces.Components;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services;
using TFN.Domain.Services.Providers;
using TFN.Domain.Services.Validators;
using TFN.Infrastructure.Components.Storage;
using TFN.Infrastructure.Repositories.ClientAggregate.InMemory;
using TFN.Infrastructure.Repositories.PostAggregate.InMemory;
using TFN.Infrastructure.Repositories.ResourceAggregate.InMemory;
using TFN.Infrastructure.Repositories.TrackAggregate.InMemory;
using TFN.Infrastructure.Repositories.TransientUserAggregate.InMemory;
using TFN.Infrastructure.Repositories.UserAggregate.InMemory;
using TFN.Mvc.Extensions;
using TFN.Resolution.Extensions;

namespace TFN.Resolution
{
    public static class Resolver
    {
        public static void RegisterTypes(IServiceCollection services, IConfiguration configuration)
        {
            //repositories
            services.AddTransient<IResourceRepository, ResourceInMemoryRepository>();
            services.AddTransient<IResourceStore, ResourceInMemoryRepository>();
            services.AddTransient<IUserRepository, UserInMemoryRepository>();
            services.AddTransient<ITransientUserRepository, TransientUserInMemoryRepository>();
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
            services.AddTransient<IKeyService, KeyService>();
            services.AddTransient<ITransientUserService, TransientUserService>();

            services.AddTransient<IUsersEventsService, UsersEventsService>();
            services.AddTransient<IUserIdProvider, UserIdProvider>();
            //validators
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            //components
            services.AddTransient<IS3StorageComponent, S3StorageComponent>();
            services.AddTransient<IBlobStorageComponent, BlobStorageComponent>();

            //email
            services.AddEmailService<EmailService>(options =>
            {
                options.Sender = new MailAddress(configuration["Messaging:Email:sender"]);
                options.Credentials = new NetworkCredential(configuration["Messaging:Email:Username"], configuration["Messaging:Email:Password"]);
                options.SmtpHost = configuration["Messaging:Email:SmtpHost"];
                options.SmtpPort = Convert.ToInt32(configuration["Messaging:Email:SmtpPort"]);
            });

            services.AddAccountEmailService<AccountEmailService>(options =>
            {
                options.KeyBaseUrl = configuration["Messaging:KeyBaseUrl"];
            });

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
