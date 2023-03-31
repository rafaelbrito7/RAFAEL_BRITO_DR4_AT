using Microsoft.IdentityModel.Tokens;
using Repository;
using Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Usuario
{
    public class UsuarioService
    {
        private ATContext context;
        public UsuarioService(ATContext _context) 
        { 
            this.context= _context;
        }

        public IEnumerable<Entidades.Usuario> ObterUsuarios()
        {
            return context.Usuarios;
        }

        public Entidades.Usuario ObterUsuarioPorId(Guid id)
        {
            return context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public Entidades.Usuario SalvarUsuario(Entidades.Usuario usuario)
        {
            var usuarioDb = this.context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);

            if (usuarioDb != null)
                throw new BusinessException("Email já cadastrado na base de dados! Por favor, utilize outro.");

            usuario.CriptografarPassword();

            this.context.Usuarios.Add(usuario);
            this.context.SaveChanges();

            return usuario;
        }

        public Entidades.Usuario AtualizarUsuario(Guid id, Entidades.Usuario usuario)
        {
            var usuarioDb = this.context.Usuarios.FirstOrDefault(x => x.Id == id);

            if (usuarioDb == null)
                throw new BusinessException("Usuário não encontrado! Não é possível realizar uma atualização.");

            usuarioDb.Email = usuario.Email;
            usuarioDb.DtNascimento = usuario.DtNascimento;
            usuarioDb.Password = usuario.Password;
            usuarioDb.Nome = usuario.Nome;

            usuarioDb.CriptografarPassword();

            this.context.Usuarios.Update(usuarioDb);
            this.context.SaveChanges();

            return usuarioDb;
        }

        public void ExcluirUsuario(Entidades.Usuario usuario)
        {
            this.context.Usuarios.Remove(usuario);
            this.context.SaveChanges();
        }

        public Entidades.Usuario AutenticarUsuario(string email, string password)
        {
            var passwordEncoded = Convert.ToBase64String(Encoding.Default.GetBytes(password));

            var user = this.context.Usuarios.FirstOrDefault(
                x => x.Email == email && x.Password == passwordEncoded
            );

            return user;
        }
    }
}
