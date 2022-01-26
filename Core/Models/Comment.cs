using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public Hotel Hotel { get; set; }
        public User User { get; set; }
        public byte Mark { get; set; }
        public string feedback { get; set; }
    }
}
