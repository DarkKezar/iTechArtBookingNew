using AutoMapper;
using Core.FormModels;
using Core.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly AccountRepository Repository;
        
        public SignInController(BookingContext bookingContext, UserManager<User> userManager)
        {
            Repository = new AccountRepository(bookingContext, userManager);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([Required]SignIn data)
        {
            return await Repository.SignIn(data);
        }
    }
}
