namespace ActivityTracker.Utility
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ActivityTracker.Models;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json.Linq;
    using System.Linq;

    public class Auth0UserInfo
    {
        public static async Task<User> GetUserInfo(string accessToken)
        {
            var getToken = new HttpClient();
            getToken.DefaultRequestHeaders.Clear();
            getToken.DefaultRequestHeaders.Add("Authorization", accessToken);
            var value = await getToken.GetStringAsync("https://ppog.auth0.com/userinfo");
            JObject record = JObject.Parse(value);
            var email = record["email"].Value<string>();
            var uniqueID = record["sub"].Value<string>();
            var firstName = record["given_name"].Value<string>();
            var lastName = record["family_name"].Value<string>();
            var picture = record["picture"].Value<string>();
            var nickname = record["nickname"].Value<string>();
            var user = new User

            {
                UniqueID = uniqueID,
                Email = email,
                LastName = lastName,
                FirstName = firstName,
                Photo = picture,
                Nickname = nickname
            };
            return user;
        }

        public static string GetUserID(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(accessToken);
            var subject = token.Subject;
            var foundClaim = (from x in token.Claims
                              where x.Type == "sub"
                              select x);
            return subject;
        }


    }
}