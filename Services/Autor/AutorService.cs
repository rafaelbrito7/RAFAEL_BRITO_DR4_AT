using Repository;
using Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Autor
{
    public class AutorService
    {
        private ATContext context;
        public AutorService(ATContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<Entidades.Autor> ObterAutores()
        {
            return context.Autores;
        }

        public Entidades.Autor ObterAutorPorId(Guid id)
        {
            return context.Autores.FirstOrDefault(x => x.Id == id);
        }

        public Entidades.Autor SalvarAutor(Entidades.Autor autor)
        {
            var autorDb = this.context.Autores.FirstOrDefault(x => x.Email == autor.Email);

            if (autorDb != null)
                throw new BusinessException("Email já cadastrado na base de dados! Por favor, utilize outro.");

            this.context.Autores.Add(autor);
            this.context.SaveChanges();

            return autor;
        }

        public Entidades.Autor AtualizarAutor(Guid id, Entidades.Autor autor)
        {
            var autorDb = this.context.Autores.FirstOrDefault(x => x.Id == id);

            if (autorDb == null)
                throw new BusinessException("Autor não encontrado! Não é possível realizar uma atualização.");

            autorDb.Nome = autor.Nome;
            autorDb.Sobrenome = autor.Sobrenome;
            autorDb.Email = autor.Email;
            autorDb.DtNascimento = autor.DtNascimento;

            this.context.Autores.Update(autorDb);
            this.context.SaveChanges();

            return autorDb;
        }

        public void ExcluirAutor(Entidades.Autor autor)
        {
            this.context.Autores.Remove(autor);
            this.context.SaveChanges();
        }
    }
}

