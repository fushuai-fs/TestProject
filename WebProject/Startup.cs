using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebProject;

[assembly: OwinStartup(typeof(Startup))]

namespace WebProject
{
    public class Startup
    {
        public void ConfigreOAuth(IAppBuilder app)
        {
            var oauthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new Providers.SimpleAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(oauthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigreOAuth(app);
               var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            //添加这行
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);


            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //要编写缩进的JSON
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;


            //处理循环引用，要在JSON中保留对象引用
            /***例如********/
            //    public class Employee
            //{
            //    public string Name { get; set; }
            //    public Department Department { get; set; }
            //}

            //public class Department
            //{
            //    public string Name { get; set; }
            //    public Employee Manager { get; set; }
            //}

            //public class DepartmentsController : ApiController
            //{
            //    public Department Get(int id)
            //    {
            //        Department sales = new Department() { Name = "Sales" };
            //        Employee alice = new Employee() { Name = "Alice", Department = sales };
            //        sales.Manager = alice;
            //        return sales;
            //    }
            //}
            //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling =
            //        Newtonsoft.Json.PreserveReferencesHandling.All;
        }
    }
}