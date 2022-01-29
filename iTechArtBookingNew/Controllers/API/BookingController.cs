using Core.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingRepository Repository;
        private readonly UserManager<User> _userManager;

        public BookingController(BookingContext bookingContext, UserManager<User> userManager)
        {
            Repository = new BookingRepository(bookingContext);
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid roomId, DateTime startDate, DateTime endDate)
        {
            return await Repository.Add((await _userManager.GetUserAsync(HttpContext.User)).Id, roomId, startDate, endDate);
        }

        [HttpGet]
        public async Task<List<Booking>> Get(int page)
        {
            return await Repository.Get((await _userManager.GetUserAsync(HttpContext.User)).Id, page);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid bookingId)
        {
            return await Repository.Delete((await _userManager.GetUserAsync(HttpContext.User)).Id, bookingId);
        }
    }
}
