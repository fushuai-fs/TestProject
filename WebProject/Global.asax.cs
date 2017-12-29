using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace WebProject
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
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

        //MVC的WebApi开启Session会话支持
        //public override void Init()
        //{
        //    //PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
        //    this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        //    base.Init();
        //}
        //// MVC的WebApi开启Session会话支持
        //void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        //}

    }
}