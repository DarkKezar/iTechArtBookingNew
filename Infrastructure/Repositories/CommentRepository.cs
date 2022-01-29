using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CommentRepository
    {
        private readonly BookingContext db;
        public CommentRepository(BookingContext bookingContext)
        {
            db = bookingContext;
        }

        public async Task<IActionResult> Add(Guid userId, Guid hotelId, string feedback, byte mark)
        {
            var User = await db.Users.FirstAsync(U => U.Id == userId);
            var Hotel = await db.Hotels.FirstAsync(H => H.Id == hotelId);

            if (User != null && Hotel != null)
            {
                await db.Comments.AddAsync(new Comment()
                {
                    User = User,
                    Hotel = Hotel,
                    feedback = feedback,
                    Mark = mark
                });
                await db.SaveChangesAsync();
                return new OkResult();
            }
            else return new NotFoundResult();
        }

        public async Task<List<Comment>> Get(Guid hotelId, int page)
        {
            if (page < 1) page = 1;
            return await db.Comments.Where(C => C.Hotel.Id == hotelId).Skip((page - 1) * 8).Take(8).ToListAsync();
        }


    }
}
