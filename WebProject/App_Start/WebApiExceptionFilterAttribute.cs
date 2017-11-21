using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebProject.App_Start
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class WebApiExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // httpError
            // actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,"");


            string strError = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" +
               actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "|Stack information：" +
               actionExecutedContext.Exception.StackTrace;

            if (actionExecutedContext.Exception is NotImplementedException)
            {
                var oResponse = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                oResponse.Content = new StringContent("Not Supported");
                oResponse.ReasonPhrase = "This Func is Not Supported";
                actionExecutedContext.Response = oResponse;
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            else if (actionExecutedContext.Exception is DivideByZeroException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else if (actionExecutedContext.Exception is NotFiniteNumberException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
             
            else
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }




            base.OnException(actionExecutedContext);
        }
    }
}