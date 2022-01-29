using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
