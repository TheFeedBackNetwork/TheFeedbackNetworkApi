using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Akka.Actor;
using IdentityModel;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TFN.ActorSystem;
using TFN.ActorSystem.Actors.PostsSystem;
using TFN.ActorSystem.Actors.UsersSystem;
using TFN.Infrastructure.Modules;
using TFN.Resolution;
using TFN.Mvc.Constants;
using TFN.Mvc.Extensions;

namespace TFN.Api
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }
        private Akka.Actor.ActorSystem ActorSystem { get; }
        public Startup(IHostingEnvironment env)
        {
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment() || env.IsLocal())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            ActorSystem = Akka.Actor.ActorSystem.Create("tfn-system", File.ReadAllText($"Config.{env.EnvironmentName}.hocon"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Resolver.RegisterTypes(services);
            Resolver.RegisterAuthorizationPolicies(services);

            SystemActors.PostsSystemActor = ActorSystem.ActorOf(Props.Create(() => new PostsSystemActor()),
                "posts-system");

            SystemActors.UserSystemActor = ActorSystem.ActorOf(Props.Create(() => new UsersSystemActor()),
                "users-system");

            services.AddSingleton<Akka.Actor.ActorSystem>(_ => ActorSystem);

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                });

            services.AddIdentityServer(options =>
                {
                    options.UserInteraction = new UserInteractionOptions
                    {
                        LoginUrl = "/" + RoutePaths.SignInUrl,
                        LogoutUrl = "/" + RoutePaths.SignOutUrl,
                        ConsentUrl = "/" + RoutePaths.ConsentUrl,
                        ErrorUrl = "/" + RoutePaths.ErrorUrl
                    };

                })
                .AddInMemoryPersistedGrants()
                .AddTemporarySigningCredential();

            

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsBuilder =>
                {
                    corsBuilder.AllowAnyHeader();
                    corsBuilder.AllowAnyMethod();
                    corsBuilder.AllowAnyOrigin();
                    corsBuilder.AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStopping.Register(ApplicationStopping);

            if (!env.IsLocal())
            {
                loggerFactory.AddAppendBlob(
                    Configuration["Logging:StorageAccountConnectionString"],
                    LogLevel.Information);
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseCors("CorsPolicy");
            app.UseDeveloperExceptionPage();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = Configuration["Authorization:Authority"],
                Audience = Configuration["Authorization:Audience"],
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,

                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.PreferredUserName,
                    RoleClaimType = JwtClaimTypes.Role,
                }
            });

            //STS Fork
            app.Map("/identity", identity =>
            {
                identity.UseIdentityServer();
                identity.UseStaticFiles();
                identity.UseMvcWithDefaultRoute();
            });

            //API Fork
            
            //app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseApplicationInsightsExceptionTelemetry();

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("The Feedback Network API application configuration complete.");
        }

        protected void ApplicationStopping()
        {
            
        }
    }
}
