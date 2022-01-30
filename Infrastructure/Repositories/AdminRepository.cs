using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository
    {
        private readonly BookingContext db;
        private readonly UserManager<User> _userManeger;

        public AdminRepository(BookingContext bookingContext, UserManager<User> userManager)
        {
            db = bookingContext;
            _userManeger = userManager;
        }
        public async Task<IActionResult> AddAdmin(Guid userId)
        {
            var User = await db.Users.FirstAsync(U => U.Id == userId);
            if (User != null)
            {
                if(await _userManeger.IsInRoleAsync(User, "admin"))
                {
                    return new StatusCodeResult(401); //Change Status Code
                }
                else
                {
                    await _userManeger.AddToRoleAsync(User, "admin");
                    return new OkResult();
                }
            }
            else return new NotFoundResult();
        }
        public async Task<List<User>> GetUsers(int page)
        {
            if (page < 1) page = 1;
            return await db.Users.Skip((page - 1) * 8).Take(8).ToListAsync();
        }

        public async Task<List<Booking>> GetBookings(int page)
        {
            if (page < 1) page = 1;
            return await db.Booking.Skip((page - 1) * 8).Take(8).ToListAsync();
        }
    }
}
