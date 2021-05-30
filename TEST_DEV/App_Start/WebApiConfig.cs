using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TEST_DEV.Handlers;

namespace TEST_DEV
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new ValidarTokenHandler());
        }
    }
}
