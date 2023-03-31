using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Livro;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private LivroService Service { get; set; }

        public LivrosController(LivroService service)
        {
            Service = service;
        }

        // GET: api/<LivrosController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.Service.ObterLivros());
        }

        // GET api/<LivrosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var book = this.Service.ObterLivroPorId(id);
            return book == null ? NotFound() : Ok(book);
        }

        // POST api/<LivrosController>
        [HttpPost]
        public IActionResult Post([FromBody] Livro livro)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var book = this.Service.SalvarLivro(livro);
                return Created($"/livros/{book.Id}", book);
            }
            catch (Exception exception)
            {
                return UnprocessableEntity(exception);
            }

        }

        // PUT api/<LivrosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Livro livro)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var book = this.Service.AtualizarLivro(id, livro);
                return Ok(book);
            }
            catch(Exception exception)
            {
                return UnprocessableEntity(exception);
            }
        }

        // DELETE api/<LivrosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var book = this.Service.ObterLivroPorId(id);

            if (book == null)
                return NotFound();

            this.Service.ExcluirLivro(book);

            return NoContent();
        }
    }
}
