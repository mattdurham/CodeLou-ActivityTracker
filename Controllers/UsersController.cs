using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityTracker.Database;
using ActivityTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        [Authorize]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            using (var context = new ActivityContext())
            {
                return context.Users;
            }
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<User> GetUserInfo()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].First();
            return await Utility.Auth0UserInfo.GetUserInfo(accessToken);
        }

        [Authorize]
        [HttpGet("/uniqueID")]
        public User GetUserInfo(string uniqueID)
        {
            using (var context = new ActivityContext())
            {
                var user = (from x in context.Users
                            where x.UniqueID == uniqueID
                            select x).FirstOrDefault();
                return user;
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<User> AddUser()
        {
            // HttpContext.User is the security information, in this case it is Auth0
            var currentUser = HttpContext.User;
            // There is an access token in the authorization header that the api verifies against Auth0 to make
            //  sure it is valid. Unfortunatly it does NOT contain user information. In that case we need get an id token
            //  that contains user profile information
            var authorizationToken = HttpContext.Request.Headers["Authorization"];
            var user = await Utility.Auth0UserInfo.GetUserInfo(authorizationToken);
            using (var context = new ActivityContext())
            {
                // TODO: check to see if user already exists
                var fullUser = await context.Users.AddAsync(user);
                context.SaveChanges();
                return fullUser.Entity;
            }

        }
    }
}
