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

        /// <summary>
        /// This function is an authorized function which means the user must be logged in. This returns a list of all users.
        /// HttpGet means that is it an GET verb and keys off of /api/Users. It is the default get verb for the controller
        /// </summary>
        /// <returns>Array of users</returns>
        [Authorize]
        [HttpGet]
        public User[] GetUsers()
        {
            using (var context = new ActivityContext())
            {
                // We convert to an array to prevent any JSON issues, the dot net framework will automatically convert
                //  the result to JSON
                return context.Users.ToArray();
            }
        }


        /// <summary>
        /// This returns information on the specific user, emails, first name, last name, photo ect. It is accessed via
        ///  /api/Users/Me
        /// </summary>
        /// <returns>JSON of a single record</returns>
        [Authorize]
        [HttpGet("Me")]
        public async Task<User> GetUserInfo()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"].First();
            return await Utility.Auth0UserInfo.GetUserInfo(accessToken);
        }

        /// <summary>
        /// Returns information on a single user defined by the uniqueid, this is the id returned from Auth0
        /// </summary>
        /// <param name="uniqueID">Auth0 unique id</param>
        /// <returns></returns>
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

        /// <summary>
        /// This is a POST verb to add a new user. Normally you would pass in the data to create the new user but in this specific case we know that information
        /// from the authorization token.
        /// </summary>
        /// <returns></returns>
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
