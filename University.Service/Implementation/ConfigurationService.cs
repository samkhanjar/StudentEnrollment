using Microsoft.Extensions.Configuration;
using University.Service.Interfaces;

namespace University.Service.Implementation
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration Configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string ConnectionString => Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }
}
