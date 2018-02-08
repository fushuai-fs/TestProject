using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebProject.Providers
{
    public class SimpleAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        //用来验证客户端的????? 
        public override   Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
              context.Validated();
            return Task.FromResult<object>(null);// return base.ValidateClientAuthentication(context);
        }
        //负责验证发送给认证服务器令牌端的用户名和密码 ??? 
        public override  Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //context.UserName, context.Password
            Models.UserModel user = null;
            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "用户名或者密码不正确"); 
            //    return base.GrantResourceOwnerCredentials(context);
            //}
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            // 添加两个请求(sub和role)?????
            //identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //identity.AddClaim(new Claim(ClaimTypes.Role, "user")); 
            //identity.AddClaim(new Claim("sub", context.UserName));

            //var props = new AuthenticationProperties(new Dictionary<string, string>
            //    {
            //        {
            //            "as:client_id", context.ClientId ?? string.Empty
            //        },
            //        {
            //            "userName", context.UserName
            //        }
            //    });
            //var ticket = new AuthenticationTicket(identity, props);
            //生成令牌
            //context.Validated(ticket);
            context.Validated(identity);
            return base.GrantResourceOwnerCredentials(context);
        }

        //public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        //{
        //    foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
        //    {
        //        context.AdditionalResponseParameters.Add(property.Key, property.Value);
        //    }

        //    return Task.FromResult<object>(null);
        //}


        //public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    string clientId;
        //    string clientSecret;
        //    context.TryGetFormCredentials(out clientId, out clientSecret);

        //    if (clientId == "1234" && clientSecret == "5678")
        //    {
        //        context.Validated(clientId);
        //    }

        //    return base.ValidateClientAuthentication(context);
        //}

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "iOS App"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            return base.GrantClientCredentials(context);
        }
    }
}