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
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        //负责验证发送给认证服务器令牌端的用户名和密码 ???
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //context.UserName, context.Password
            Models.UserModel user = null;
            if (user == null)
            {
                context.SetError("invalid_grant", "用户名或者密码不正确");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            // 添加两个请求(sub和role)?????
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            //生成令牌
            context.Validated(identity);
            
        }
    }
}