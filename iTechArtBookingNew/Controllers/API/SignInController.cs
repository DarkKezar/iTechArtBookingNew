﻿using AutoMapper;
using Core.FormModels;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {

        private readonly UserManager<User> _userManager;

        public SignInController(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn data)
        {
            var user = ModelToUser(data);
            using (var db = new BookingContext())
            {
                if (db.Users.Count(u => u.Email == user.Email) != 0)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Email is already used!" });
                if (db.Users.Count(u => u.NormalizedUserName == user.UserName.ToUpper()) != 0)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "UserName is already used!" });
            }

            var result = await _userManager.CreateAsync(user, data.Password);
            if (result == null) return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new
                        {
                            Message = "User creation failed!",
                            Errors = result.Errors
                        });
            await _userManager.AddToRoleAsync(user, "user");
            return Ok(new
            {
                Status = "Success",
                Message = "User created successfully!"
            });
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