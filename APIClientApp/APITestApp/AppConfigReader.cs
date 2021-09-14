// This allows us to access the app.config via the key
using System.Configuration;

namespace APITestApp
{
    public class AppConfigReader
    {
        public static readonly string BaseUrl = ConfigurationManager.AppSettings["base_url"];
    }
}
