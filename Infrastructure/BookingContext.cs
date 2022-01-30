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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role() { Id = new Guid("d494e13e-b8fa-40d5-3ac8-08d9e18a2731"), Name = "user", ConcurrencyStamp = "64dfa0c3-291d-4adf-b8e7-06c0cac7c8d2", NormalizedName = "USER" },
                new Role() { Id = new Guid("7fd1dcd6-92e3-4a6e-3ac9-08d9e18a2731"), Name = "admin", ConcurrencyStamp = "2fb2b213-cd3c-4461-9280-26fceb5150e5", NormalizedName = "ADMIN" }
           );
        }

        public BookingContext()
        { }
    }
}
