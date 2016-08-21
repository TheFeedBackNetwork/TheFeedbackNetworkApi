using Microsoft.AspNetCore.Hosting;
using System;

namespace TFN.Mvc.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsLocal(this IHostingEnvironment env)
        {
            return env.EnvironmentName.Equals("Local", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsTest(this IHostingEnvironment env)
        {
            return env.EnvironmentName.Equals("Test", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}