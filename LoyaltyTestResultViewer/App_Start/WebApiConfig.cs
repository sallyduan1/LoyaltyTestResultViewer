﻿using System.Web.Http;

namespace LoyaltyTestResultViewer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{lastDays}",
                defaults: new { lastDays = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "getTestCases",
                routeTemplate: "api/{controller}/{fileName}/{column}"
            );
        }
    }
}
