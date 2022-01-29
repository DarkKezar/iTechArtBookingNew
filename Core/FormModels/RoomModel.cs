using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FormModels
{
    public class RoomModel
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public IFormFile Img { get; set; }
    }
}
