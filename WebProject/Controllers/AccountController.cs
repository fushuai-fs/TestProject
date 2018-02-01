using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class AccountController : ApiController
    {
        //POST /api/Acount/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            IHttpActionResult errorResult = null;
            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }
    }
}
