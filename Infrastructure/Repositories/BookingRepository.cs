using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookingRepository
    {
        private readonly BookingContext db;
        public BookingRepository(BookingContext bookingContext)
        {
            db = bookingContext;
        }

        public async Task<IActionResult> Add(Guid userId, Guid roomId, DateTime startDate, DateTime endDate)
        {
            var User = await db.Users.FirstAsync(U => U.Id == userId);
            var Room = await db.Rooms.FirstAsync(R => R.Id == roomId);

            if (Room != null)
            {
                if (await db.Booking.Where(b => b.StartDate <= endDate && b.EndDate >= startDate).CountAsync() == 0)
                {
                    await db.Booking.AddAsync(new Booking()
                    {
                        Room = Room,
                        User = User,
                        StartDate = startDate,
                        EndDate = endDate
                    });
                    await db.SaveChangesAsync();
                    return new OkResult();
                }
                else return new StatusCodeResult(404); //To change
            }
            else return new NotFoundResult();

        }
        public async Task<List<Booking>> Get(Guid userId, int page)
        {
            if (page < 1) page = 1;
            return await db.Booking.Where(B => B.User.Id == userId).Include(B => B.Room).Skip((page - 1) * 8).Take(8).ToListAsync();
        }

        public async Task<IActionResult> Delete(Guid userId, Guid bookingId)
        {
            var Booking = await db.Booking.FirstAsync(B => B.Id == bookingId);
            if (Booking != null)
            {
                if (Booking.User.Id == userId)
                {
                    db.Booking.Remove(Booking);
                    await db.SaveChangesAsync();
                    return new OkResult();
                }
                else return new StatusCodeResult(401);
            }
            else return new NotFoundResult();
        }
    }
}
