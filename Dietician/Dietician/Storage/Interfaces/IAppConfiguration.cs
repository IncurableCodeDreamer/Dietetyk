using Microsoft.Extensions.Configuration;

namespace Dietician.Storage
{
    public interface IAppConfiguration
    {
        string GetConnectionString(string name);

        string GetVariable(string name);

         string GetEnvironmentVariable(string name);

         IConfigurationSection GetSection(string key);
    }
}