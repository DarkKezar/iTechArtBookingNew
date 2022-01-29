using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Room
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string ImgSrc { get; set; }
        [JsonIgnore]
        public Hotel Hotel { get; set; }
    }
}
