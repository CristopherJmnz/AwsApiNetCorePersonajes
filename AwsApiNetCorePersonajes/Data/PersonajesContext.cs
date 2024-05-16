using AwsApiNetCorePersonaje.Models;
using Microsoft.EntityFrameworkCore;

namespace AwsApiNetCorePersonajes.Data
{
    public class PersonajesContext : DbContext
    {
        public PersonajesContext(DbContextOptions<PersonajesContext> options) : base(options)
        {

        }


        public DbSet<Personaje> Personajes { get; set; }
    }
}
