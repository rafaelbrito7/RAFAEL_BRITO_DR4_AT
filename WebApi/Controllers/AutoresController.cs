using Entidades;
using Microsoft.AspNetCore.Mvc;
using Services.Autor;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private AutorService Service { get; set; }

        public AutoresController(AutorService service)
        {
            Service = service;
        }

        // GET: api/<AutoresController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.Service.ObterAutores());
        }

        // GET api/<AutoresController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var author = this.Service.ObterAutorPorId(id);
            return author == null ? NotFound() : Ok(author);
        }

        // POST api/<AutoresController>
        [HttpPost]
        public IActionResult Post([FromBody] Autor autor)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var author = this.Service.SalvarAutor(autor);
                return Created($"/autores/{author.Id}", author);
            } catch(Exception exception)
            {
                return UnprocessableEntity(exception);
            }
        }

        // PUT api/<AutoresController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Autor autor)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var author = this.Service.AtualizarAutor(id, autor);
                return Ok(author);
            }
            catch(Exception exception)
            {
                return UnprocessableEntity(exception);
            }
        }

        // DELETE api/<AutoresController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var author = this.Service.ObterAutorPorId(id);

            if (author == null)
                return NotFound();

            this.Service.ExcluirAutor(author);

            return NoContent();
        }
    }
}
