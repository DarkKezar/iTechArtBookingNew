using Core.FormModels;
using Core.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomRepository Repository;

        public RoomController(BookingContext bookingContext, IHostEnvironment environment)
        {
            Repository = new RoomRepository(bookingContext, environment);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] RoomModel data)
        {
            return await Repository.Add(data);
        }

        [HttpGet]
        public async Task<List<Room>> Get(Guid hotelId, int page)
        {
            return await Repository.Get(hotelId, page);
        }
    }
}
