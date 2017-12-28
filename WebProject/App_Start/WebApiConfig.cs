using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebProject
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Microsoft.AspNet.WebApi.Cors 解决跨域方式之一 
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new WebApiExceptionFilterAttribute());
        }
    }
}
