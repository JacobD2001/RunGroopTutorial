using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RunGroopTutorial.Models;

namespace RunGroopTutorial.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser> //inheriting entityframework in some way + identity fw
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //ctor, meaning pass options to dbcontext
        {

        }
        public DbSet<Race> Races { get; set; } //A DbSet represents the collection of all entities in the context, or that can be queried from the database, of a given type.
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Adress> Addresses { get; set; }
/*        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }*/




    }
}
