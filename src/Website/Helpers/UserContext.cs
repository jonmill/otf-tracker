using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using OtfTracker.Common.Responses;
using OtfTracker.Website.Identity;

namespace OtfTracker.Website.Helpers
{
    public static class UserContext
    {
        public static async Task SignInOtfUserAsync(this HttpContext context, LoginResponse login)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, login.Email),
                new Claim(ClaimTypes.GivenName, login.GivenName),
                new Claim(ClaimTypes.Name, login.FamilyName),
                new Claim(ClaimTypes.NameIdentifier, login.MemberId),
                new Claim("Jwt", login.JwtToken),
                new Claim("HomeStudioId", login.HomeStudioId)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties props = new AuthenticationProperties()
            {
                ExpiresUtc = login.Expiration,
                IsPersistent = true,
                IssuedUtc = login.IssuedOn,
            };
            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                props);
        }

        public static Task SignOutOtfUserAsync(this HttpContext context)
        {
            return context.SignOutAsync();
        }

        public static OtfUser GetSignedInOtfUser(this HttpContext context)
        {
            ClaimsIdentity id = context.User.Identities.SingleOrDefault(i => i.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme);
            if (id == null)
            {
                return null;
            }
            else
            {
                return new OtfUser()
                {
                    Email = id.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
                    FamilyName = id.Claims.Single(c => c.Type == ClaimTypes.Name).Value,
                    GivenName = id.Claims.Single(c => c.Type == ClaimTypes.GivenName).Value,
                    HomeStudioId = id.Claims.Single(c => c.Type == "HomeStudioId").Value,
                    SignInJwt = id.Claims.Single(c => c.Type == "Jwt").Value,
                    MemberId = id.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
                };
            }
        }
    }
}
