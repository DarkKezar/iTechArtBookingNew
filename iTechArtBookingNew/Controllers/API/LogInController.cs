using Core.FormModels;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly BookingContext _bookingContext;
        private readonly UserManager<User> _userManager;

        public LogInController(BookingContext bookingContext, UserManager<User> userManager)
        {
            _bookingContext = bookingContext;
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([Required] LogIn data)
        {
            var user = await _userManager.FindByNameAsync(data.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, data.Password))
            {
                var authClaims = new List<Claim>();

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: new List<Claim>
                                {
                                    new Claim(ClaimsIdentity.DefaultNameClaimType, data.UserName),
                                    new Claim(ClaimsIdentity.DefaultRoleClaimType, (await _userManager.GetRolesAsync(user)).ToString())
                                },
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = user.UserName
                };

                return Ok( encodedJwt );
            }
            return Unauthorized(new { Message = "Wrong login or password!!!" });

        }
    }
}
