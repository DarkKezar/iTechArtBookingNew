using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    public class BookingContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        { }

        public BookingContext()
        { }
    }
}
