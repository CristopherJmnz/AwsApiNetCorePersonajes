using AwsApiNetCorePersonaje.Models;
using AwsApiNetCorePersonajes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwsApiNetCorePersonajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private PersonajesRepository repo;
        public PersonajesController(PersonajesRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Personaje>>> GetPersonajes()
        {
            return Ok(await this.repo.GetPersonajesAsync());
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Personaje>> FindPersonaje(int id)
        {
            Personaje pers = await this.repo.FindPersonajeAsync(id);
            return pers != null ? Ok(pers) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Personaje personaje)
        {
            await this.repo.InsertPersonajeAsync(personaje.Nombre, personaje.Imagen);
            return Ok();
        }
    }
}
