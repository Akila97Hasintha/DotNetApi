using System.Web.Http;
using System.Web.Http.Cors;

namespace ContosoUniversity.Api
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Other configurations...

            // Enable CORS
            var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(cors);

            // Your existing route configuration...
        }
    }
}
