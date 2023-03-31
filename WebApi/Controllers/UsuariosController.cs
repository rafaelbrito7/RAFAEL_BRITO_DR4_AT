using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services.Exception;
using Services.Usuario;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private UsuarioService Service { get; set; }

        public UsuariosController(UsuarioService service)
        {
            Service = service;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.Service.ObterUsuarios());
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var user = this.Service.ObterUsuarioPorId(id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var user = this.Service.SalvarUsuario(usuario);
                return Created($"/usuarios/{usuario.Id}", user);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Usuario usuario)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var user = this.Service.AtualizarUsuario(id, usuario);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }
        

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = this.Service.ObterUsuarioPorId(id);

            if (user == null)
                return NotFound();

            this.Service.ExcluirUsuario(user);

            return NoContent();
        }
    }
}
