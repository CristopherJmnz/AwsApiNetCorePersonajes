using AwsApiNetCorePersonaje.Models;
using AwsApiNetCorePersonajes.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

namespace AwsApiNetCorePersonajes.Repositories
{
    public class PersonajesRepository
    {
        private PersonajesContext context;
        public PersonajesRepository(PersonajesContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            List<Personaje> personajes = await this.context.Personajes
                .ToListAsync();
            return personajes.Count == 0 ? null : personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            return await this.context.Personajes
                .FirstOrDefaultAsync(x => x.IdPersonaje == id);
        }
        private async Task<int> GetMaxIdPersonajeAsync()
        {
            return await this.context.Personajes
                .MaxAsync(x => x.IdPersonaje) + 1;
        }

        public async Task InsertPersonajeAsync
            (string nombre, string imagen)
        {
            Personaje pers = new Personaje()
            {
                IdPersonaje = await this.GetMaxIdPersonajeAsync(),
                Imagen = imagen,
                Nombre = nombre
            };
            await this.context.Personajes.AddAsync(pers);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePersonaje(int id,string nombre, string imagen)
        {
            string sql = "Call updateP (@p_id,@p_nombre,@p_imagen)";
            MySqlParameter pamId = new MySqlParameter("@p_id",id);
            MySqlParameter pamNombre = new MySqlParameter("@p_nombre",nombre);
            MySqlParameter pamImagen = new MySqlParameter("@p_imagen",imagen);
            await this.context.Database.ExecuteSqlRawAsync(sql,pamId,pamNombre,pamImagen);
            await this.context.SaveChangesAsync();
        }

    }
}
