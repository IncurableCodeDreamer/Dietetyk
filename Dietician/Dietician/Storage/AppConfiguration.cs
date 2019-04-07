using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Dietician.Storage
{
    public class AppConfiguration : IAppConfiguration
    {

        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, string> _configDictionary = new Dictionary<string, string>

        {
            {"UserTable","userTable" }
        };

        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(_configDictionary)
                .AddConfiguration(configuration)
                .Build();
        }

        public string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }

        public string GetVariable(string name)
        {
            return _configuration[name];
        }

        public string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }

        public IConfigurationSection GetSection(string key)
        {
            return _configuration.GetSection(key);
        }
    }
}
