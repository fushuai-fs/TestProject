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