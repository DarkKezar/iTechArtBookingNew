using Core.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly AdminRepository Repository;
        private readonly UserManager<User> _userManager;

        public AdminController(BookingContext bookingContext, UserManager<User> userManager)
        {
            Repository = new AdminRepository(bookingContext, userManager);
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
        }

        [Route("Users")]
        [HttpPost]
        public async Task<IActionResult> AddAdmin(Guid userId)
        {
            return await Repository.AddAdmin(userId);
        }

        [Route("Users")]
        [HttpGet]
        public async Task<List<User>> GetUsers(int page)
        {
            return await Repository.GetUsers(page);
        }

        [Route("Booking")]
        [HttpGet]
        public async Task<List<Booking>> GetBookings(int page)
        {
            return await Repository.GetBookings(page);
        } 
    }
}
