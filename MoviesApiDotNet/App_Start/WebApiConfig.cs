using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MoviesApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web


            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());


            config.EnableCors();

            var settings =
                    config.Formatters.JsonFormatter.SerializerSettings;
                settings.ContractResolver = new
                    CamelCasePropertyNamesContractResolver();
                settings.Formatting = Formatting.Indented;

            GlobalConfiguration.Configuration.Formatters
                    .JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling
                = ReferenceLoopHandling.Ignore;


            


            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
