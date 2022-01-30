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
    public class DeleteAccountController : ControllerBase
    {
        private readonly AccountRepository Repository;
        private readonly UserManager<User> _userManager;
        public DeleteAccountController(BookingContext bookingContext, UserManager<User> userManager)
        {
            Repository = new AccountRepository(bookingContext, userManager);
            _userManager = userManager ?? throw new ArgumentException("Invalid argument.");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return await Repository.Delete((await _userManager.GetUserAsync(HttpContext.User)).Id);
        }
    }
}
