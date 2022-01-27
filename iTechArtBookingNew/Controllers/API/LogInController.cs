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
            User User = await _userManager.FindByEmailAsync(data.Email.Normalize());
            if (User == null)
            {
                return new NotFoundObjectResult($"User with email '{data.Email}' was not found.");
            }

            if (await _userManager.CheckPasswordAsync(User, data.Password))
            {
                var UserRoles = await _userManager.GetRolesAsync(User);
                var Claims = GetClaims(User, UserRoles);
                var JwtToken = GetNewToken(Claims);

                return new OkObjectResult(new
                {
                    claims = Claims,
                    roles = UserRoles,
                    token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                    expiration = JwtToken.ValidTo,
                    id = User.Id
                });
            }
            else
            {
                return new UnauthorizedObjectResult("Invalid password.");
            }

        }

        private JwtSecurityToken GetNewToken(List<Claim> Claims)
        {
            return new JwtSecurityToken(issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
        }

        private List<Claim> GetClaims(User user, IList<string> UserRoles)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            foreach (string role in UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
