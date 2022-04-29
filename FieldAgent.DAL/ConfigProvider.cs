using Microsoft.Extensions.Configuration;

namespace FieldAgent.DAL
{
    public class ConfigProvider
    {
        public IConfigurationRoot Config { get; private set; }
        public ConfigProvider()
        {
            var builder = new ConfigurationBuilder();

            //Locates location of db info
            builder.AddUserSecrets<AppDbContext>();
            Config = builder.Build();
        }
    }
}
