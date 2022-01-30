using Core.FormModels;
using Core.Models;
using Infrastructure;
using Infrastructure.Repositories;
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
        private readonly AccountRepository Repository;

        public LogInController(BookingContext bookingContext, UserManager<User> userManager)
        {
            Repository = new AccountRepository(bookingContext, userManager);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromForm][Required] LogIn data)
        {
            return await Repository.LogIn(data);
        }      
    }
}
