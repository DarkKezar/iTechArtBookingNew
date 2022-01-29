using Core.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentRepository Repository;
        private readonly UserManager<User> _userManager;

        public CommentController(BookingContext bookingContext, UserManager<User> userManager)
        {
            Repository = new CommentRepository(bookingContext);
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid hotelId, string feedback, byte mark)
        {
            return await Repository.Add((await _userManager.GetUserAsync(HttpContext.User)).Id, hotelId, feedback, mark);
        }

        public async Task<List<Comment>> Get(Guid hotelId, int page)
        {
            return await Repository.Get(hotelId, page);
        }
    }
}
