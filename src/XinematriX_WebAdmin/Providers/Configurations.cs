using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using XinematriX.Models.Models;

namespace XinematriX.WebAdmin.Providers
{
    public class Configurations
    {
        private readonly IOptions<AppConfig> _config;
        public Configurations(IOptions<AppConfig> config)
        {
            _config = config;
        }

        public AppConfig GetApiConString()
        {
            return _config.Value;

        }
    }
}
