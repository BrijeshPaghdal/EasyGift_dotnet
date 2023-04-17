using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace EasyGift_API.Repository
{
    public class StoredConnection
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        readonly static string connectionString = configuration.GetConnectionString("DefaultSQLConnection");

        public static string GetConnection()
        {
            return connectionString;
        }
    }
}
