using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Threading.Tasks;
using Akka.Actor;
using IdentityModel;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TFN.ActorSystem;
using TFN.ActorSystem.Actors.PostsSystem;
using TFN.ActorSystem.Actors.SignalRBridge;
using TFN.ActorSystem.Actors.UsersSystem;
using TFN.Api.Extensions;
using TFN.Api.Filters.ActionFilters;
using TFN.Api.UI;
using TFN.Domain.Services;
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
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
                        
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsLocal())
            {
                builder.AddUserSecrets();


            }

            if (env.IsDevelopment() || env.IsLocal())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            if (!env.IsLocal())
            {
                loggerFactory.AddAppendBlob(
                    Configuration["Logging:StorageAccountConnectionString"],
                    LogLevel.Information);

                loggerFactory.AddEmail(
                    Configuration["Logging:Email:SupportEmail"],
                    Configuration["Logging:Email:Username"],
                    Configuration["Logging:Email:Username"],
                    Configuration["Configuration:Email:Password"],
                    Configuration["Logging:Email:Host"],
                    Convert.ToInt32(Configuration["Logging:Email:Port"]),
                     env.EnvironmentName,
                     LogLevel.Error);
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("The Feedback Network API application configuration is starting.");

            ActorSystem = Akka.Actor.ActorSystem.Create("tfn-system", File.ReadAllText($"Config.{env.EnvironmentName}.hocon"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Resolver.RegisterTypes(services, Configuration);
            Resolver.RegisterAuthorizationPolicies(services);

            SystemActors.PostsSystemActor = ActorSystem.ActorOf(Props.Create(() => new PostsSystemActor()),
                "posts-system");

            SystemActors.UserSystemActor = ActorSystem.ActorOf(Props.Create(() => new UsersSystemActor()),
                "users-system");

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(typeof(ValidateModelFilterAttribute));
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                })
                .AddRazorOptions(options =>
                {
                    options.ViewLocationExpanders.Add(new ViewLocationExpander());
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

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime, IConnectionManager connectionManager)
        {
            appLifetime.ApplicationStopping.Register(ApplicationStopping);

            var userEventsService = new UsersEventsService(connectionManager);
            SystemActors.SignalRBridgeActor =
                ActorSystem.ActorOf(
                    Props.Create(() => new SignalRBridgeActor(userEventsService)));


            

            app.UseApplicationInsightsRequestTelemetry();

            app.UseCors("CorsPolicy");
            app.UseDeveloperExceptionPage();



            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.GetTokenFromQueryString();
                        return Task.CompletedTask;
                    } 
                },
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

            //API Fork
            app.UseDefaultFiles(new DefaultFilesOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "client")),
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "client")),
            });
            app.UseStaticFiles(new StaticFileOptions
            {

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}");
            });

            


            //STS Fork
            app.Map($"/{RoutePaths.IdentityRootBase}", identity =>
            {
                identity.UseIdentityServer();
                identity.UseStaticFiles();
                identity.UseMvcWithDefaultRoute();
            });

            app.Map($"/{RoutePaths.SignalRRootBase}", signalR =>
            {
                signalR.UseSignalR();
            });

            app.UseApplicationInsightsExceptionTelemetry();

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("The Feedback Network API application configuration complete.");
        }

        protected void ApplicationStopping()
        {
            Debug.WriteLine("Stopping Application");
          
            ActorSystem.Terminate();
        }
    }
}
