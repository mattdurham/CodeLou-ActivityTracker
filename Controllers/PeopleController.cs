using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPIApplication.Controllers
{
    [Route("people")]
    public class PeopleController : Controller
    {
        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            return Json(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpGet]
        [Route("private")]
        [Authorize]
        public async Task<IActionResult> Private()
        {
            var currentUser = HttpContext.User;
            var authorizationToken = HttpContext.Request.Headers["Authorization"];
            HttpClient getToken = new HttpClient();
            getToken.DefaultRequestHeaders.Clear();
            getToken.DefaultRequestHeaders.Add("Authorization", authorizationToken[0]);
            var value = await getToken.GetStringAsync("https://ppog.auth0.com/userinfo");
            JObject record = JObject.Parse(value);
            var email = record["email"].Value<string>();

            return Json(new
            {
                Message = $"Hello {email} from a private endpoint! You need to be authenticated to see this."
            });
        }

        /// <summary>
        /// This is a helper action. It allows you to easily view all the claims of the token
        /// </summary>
        /// <returns></returns>
        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return Json(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }
    }
}
