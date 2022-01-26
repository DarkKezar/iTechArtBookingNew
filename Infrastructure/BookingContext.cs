using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookingContext : IdentityDbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        { }

        public BookingContext()
        {
        }
    }
}
