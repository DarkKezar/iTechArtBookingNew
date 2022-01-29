using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtBookingNew.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<bool> Get()
        {
            return true;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<int> Get7()
        {
            return 7;
        }
    }
}
