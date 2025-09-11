using System;
using Microsoft.Extensions.Configuration;

namespace AdvantageShoppingTests.Utils
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot configuration;

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // ✅ safer
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        public static string Get(string key)
        {
            return configuration[$"TestSettings:{key}"];
        }
    }
}
