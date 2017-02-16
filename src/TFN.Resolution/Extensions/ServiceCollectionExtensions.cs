using System;
using Microsoft.Extensions.DependencyInjection;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Options;

namespace TFN.Resolution.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailService<TEmailService>(this IServiceCollection services,
            Action<EmailServiceOptions> configure)
            where TEmailService : class, IEmailService
        {
            services.Configure(configure);
            return services.AddSingleton<IEmailService, TEmailService>();
        }

        public static IServiceCollection AddAccountEmailService<TAccountEmailService>(this IServiceCollection services,
            Action<AccountEmailServiceOptions> configure)
            where TAccountEmailService : class, IAccountEmailService
        {
            services.Configure(configure);
            return services.AddSingleton<IAccountEmailService, TAccountEmailService>();
        }
    }
}