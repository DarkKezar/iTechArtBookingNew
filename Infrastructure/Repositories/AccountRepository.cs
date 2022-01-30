using AutoMapper;
using Core.FormModels;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using iTechArtBookingNew;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository
    {
        private readonly BookingContext db;
        private readonly UserManager<User> _userManager;

        public AccountRepository(BookingContext bookingContext, UserManager<User> userManager)
        {
            db = bookingContext;
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
        }

        public async Task<IActionResult> SignIn([Required]SignIn data)
        {
            var user = ModelToUser(data);

            var result = await _userManager.CreateAsync(user, data.Password);
            await _userManager.AddToRoleAsync(user, "user");

            return new OkResult();
        }

        public async Task<IActionResult> LogIn([Required]LogIn data)
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

        public async Task<IActionResult> Delete(Guid userId)
        {
            var User = await db.Users.FirstAsync(U => U.Id == userId);
            if (User != null)
            {
                //some code
                return new OkResult();
            }
            else return new NotFoundResult();
        }

        private User ModelToUser(SignIn model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SignIn, User>());
            var mapper = new Mapper(config);
            var User = mapper.Map<SignIn, User>(model);

            return User;
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
