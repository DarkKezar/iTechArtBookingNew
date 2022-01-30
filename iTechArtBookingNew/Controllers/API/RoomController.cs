using Core.FormModels;
using Core.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomRepository Repository;

        public RoomController(BookingContext bookingContext, IHostEnvironment environment)
        {
            Repository = new RoomRepository(bookingContext, environment);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] RoomModel data)
        {
            return await Repository.Add(data);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid roomId)
        {
            return await Repository.Delete(roomId);
        }
        [Authorize]
        [HttpGet]
        public async Task<List<Room>> Get(Guid hotelId, int page)
        {
            return await Repository.Get(hotelId, page);
        }
    }
}
