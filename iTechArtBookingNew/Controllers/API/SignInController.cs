using AutoMapper;
using Core.FormModels;
using Core.Models;
using Infrastructure;
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
        private readonly BookingContext _bookingContext;
        private readonly UserManager<User> _userManager;
        //private readonly RoleManager<Role> _roleManager;

        //public SignInController(BookingContext bookingContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        public SignInController(BookingContext bookingContext, UserManager<User> userManager)
        {
            _bookingContext = bookingContext;
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
            //_roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([Required]SignIn data)
        {
            //await _roleManager.CreateAsync(new Role { Name = "user" });
            //await _roleManager.CreateAsync(new Role { Name = "admin" });

            var user = ModelToUser(data);

            var result = await _userManager.CreateAsync(user, data.Password);
            await _userManager.AddToRoleAsync(user, "user");

            return new OkResult();
        }

        private User ModelToUser(SignIn model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SignIn, User>());
            var mapper = new Mapper(config);
            var User = mapper.Map<SignIn, User>(model);

            return User;
        }
    }
}
