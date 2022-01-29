using Core.FormModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Hosting;
using Core.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class HotelRepository
    {
        private readonly BookingContext db;
        private readonly IHostEnvironment _hostingEnvironment;

        public HotelRepository(BookingContext bookingContext, IHostEnvironment environment)
        {
            db = bookingContext;
            _hostingEnvironment = environment;
        }

        public async Task<IActionResult> Add(HotelModel data)
        {
            if (data.Img != null)
            {
                string path = "/wwwroot/HotelsImg/" + data.Img.FileName;
                using (var fileStream = new FileStream(_hostingEnvironment.ContentRootPath + path, FileMode.Create))
                {
                    await data.Img.CopyToAsync(fileStream);
                }
            }
            await db.Hotels.AddAsync(new Hotel
            {
                Name = data.Name,
                Info = data.Info,
                IngSrc = "/HotelsImg/" + data.Img.FileName
            });
            await db.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<List<Hotel>> Get(int page)
        {
            return await db.Hotels.Skip((page - 1) * 8).Take(8).ToListAsync();
        }

        public async Task<Hotel> Get(Guid id)
        {
            return await db.Hotels.Include(H => H.Rooms).AsNoTracking().FirstAsync(H => H.Id == id);
        }
    }
}
