
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAccessLayer.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BBC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DevelopController : ControllerBase
    {
        private readonly IUserService _userService;
        public DevelopController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("get-token")]
        public async Task<IActionResult> GetToken(int id = 1)
        {
            bool result = await Authenticate(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return Unauthorized(new { Message = "There is no user with this ID" } );
            }
        }

        private async Task<bool> Authenticate(int idUser)
        {
            var result = _userService.GetUserToken(idUser);
            if (result != null)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, result.Token),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, result.Role.ToString())
                };

                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
                return true;
            }
            else 
            {
                return false;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("filter-test")]
        public IActionResult FilterTest()
        {
            return Ok(new { Message = "This message is seen by the admin" } );
        }
        [HttpGet]
        [Authorize(Roles = "Guest")]
        [Route("guest-test")]
        public IActionResult GuestTest()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var result = userIdentity.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value)
                       .ToList();

            return Ok(new { Message = $"Your role: {result[0]}" });
        }
        
    }
}
