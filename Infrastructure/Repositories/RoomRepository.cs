using Core.FormModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoomRepository
    {
        private readonly BookingContext db;
        private readonly IHostEnvironment _hostingEnvironment;

        public RoomRepository(BookingContext bookingContext, IHostEnvironment environment)
        {
            db = bookingContext;
            _hostingEnvironment = environment;
        }

        public async Task<IActionResult> Add(RoomModel data)
        {
            Hotel Hotel = await db.Hotels.Include(H => H.Rooms).FirstAsync(H => H.Id == data.HotelId);
            if (Hotel != null)
            {
                if (data.Img != null)
                {
                    string path = "/wwwroot/RoomsImg/" + data.Img.FileName;
                    using (var fileStream = new FileStream(_hostingEnvironment.ContentRootPath + path, FileMode.Create))
                    {
                        await data.Img.CopyToAsync(fileStream);
                    }
                }
                
                Hotel.Rooms.Add(new Room()
                {
                    Name = data.Name,
                    Info = data.Info,
                    ImgSrc = "/RoomsImg/" + data.Img.FileName,
                    Hotel = Hotel
                });
                await db.SaveChangesAsync();
                return new OkResult();
            }
            else return new NotFoundResult();
        }

        public async Task<IActionResult> Delete(Guid roomId)
        {
            var Room = await db.Rooms.FirstAsync(R => R.Id == roomId);
            if (Room != null)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //Just to show transaction
                        File.Delete(_hostingEnvironment.ContentRootPath + "/wwwroot" + Room.ImgSrc);
                        db.RemoveRange(db.Booking.Where(B => B.Room == Room));
                        db.Remove(Room);
                        await db.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch 
                    {
                        transaction.Rollback();
                        return new StatusCodeResult(500); //Error
                    }
                }
                return new OkResult();
            }
            else return new NotFoundResult();
        }

        public async Task<List<Room>> Get(Guid hotelId, int page)
        {
            if (page < 1) page = 1;
            return await db.Rooms.Where(R => R.Hotel.Id == hotelId).Skip((page - 1) * 8).Take(8).ToListAsync();
        }
    }
}
