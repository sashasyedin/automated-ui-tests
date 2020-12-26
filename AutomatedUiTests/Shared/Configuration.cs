using Microsoft.Extensions.Configuration;

namespace AutomatedUiTests.Shared
{
    public static class Configuration
    {
        public static IConfiguration InitConfiguration()
            => new ConfigurationBuilder()
                .AddJsonFile(Constants.AppSettings)
                .Build();

    }
}