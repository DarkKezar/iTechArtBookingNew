using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Hotel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string IngSrc { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
