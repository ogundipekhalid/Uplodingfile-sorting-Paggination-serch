using Microsoft.EntityFrameworkCore;
using SercPag_sorting.Models;

namespace SercPag_sorting.Context
{
    public class PagingContext : DbContext
    {
        public PagingContext(DbContextOptions<PagingContext> options) : base(options)
        {

        }

        public DbSet<User> user { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder); // Call the base method

            modelBuilder.Entity<User>().HasData(
               new User {Id = "002" , Name = "adeyemi", Email = "ade@xyz.com" },
              new User { Id = "003", Name = "bala", Email = "bala@xyz.com" },
              new User { Id = "004", Name = "adewumi", Email = "adewumi@xyz.com" },
              new User { Id = "005", Name = "adere", Email = "adere@xyz.com" },
              new User { Id = "006", Name = "adebo", Email = "adebo@xyz.com" },
              new User { Id = "007", Name = "debo", Email = "debo@xyz.com" },
              new User { Id = "008", Name = "bumi", Email = "bumi@gmail.com" },
              new User { Id = "009", Name = "soji", Email = "soji@gmail.com" },
              new User { Id = "010", Name = "taya", Email = "taya@gmail.com" },
              new User { Id = "011", Name = "tomi", Email = "tomi@gmail.com" },
              new User { Id = "012", Name = "dami", Email = "dami@gmail.com" }
            );
        }
    }
}
