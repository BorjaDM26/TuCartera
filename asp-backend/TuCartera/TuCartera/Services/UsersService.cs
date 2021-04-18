using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TuCartera.Models;

namespace TuCartera.Services
{
    public interface IUsersService
    {
        #region Auth

        int? getLoggedUserId();
        Task doLogin(int userId, string email);
        Task doLogin(UserDTO user);
        Task doLogout();

        #endregion
    }
    public class UsersService: IUsersService
    {
        private IHttpContextAccessor _accessor;

        public UsersService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        #region Auth

        public int? getLoggedUserId()
        {
            var claims = _accessor.HttpContext.User.Claims;
            var userIdClaim = claims.Where(claim => claim.Type == "UserId").FirstOrDefault();
            if(userIdClaim != null) {
                return int.Parse(userIdClaim.Value);
            } else {
                return null;
            }
        }


        public async Task doLogin(int userId, string email)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", userId.ToString()),
                new Claim("UserEmail", email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(4)
            };

            await _accessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
        }

        public async Task doLogin(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Name", user.Name)

            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties() {
                ExpiresUtc = DateTime.UtcNow.AddHours(4)
            };

            await _accessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
        }

        public async Task doLogout()
        {
            await _accessor.HttpContext.SignOutAsync( CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #endregion
    }
}
