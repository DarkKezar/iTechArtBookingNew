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
    public class HotelController : ControllerBase
    {
        private readonly HotelRepository Repository;

        public HotelController(BookingContext bookingContext, IHostEnvironment environment)
        {
            Repository = new HotelRepository(bookingContext, environment);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] HotelModel data)
        {
            return await Repository.Add(data);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid hotelId)
        {
            return await Repository.Delete(hotelId);
        }
        
        [HttpGet]
        public async Task<List<Hotel>> Get(int page)
        {
            return await Repository.Get(page);
        }

        [Route("/api/Hotel/id/")]
        [HttpGet]
        public async Task<Hotel> Get(Guid id)
        {
            return await Repository.Get(id);
        }
    }
}
