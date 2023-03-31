using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Livro
{
    public class LivroService
    {
        private ATContext context;
        public LivroService(ATContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<Entidades.Livro> ObterLivros()
        {
            return context.Livros.Include(x => x.Autor);
        }

        public Entidades.Livro ObterLivroPorId(Guid id)
        {
            return context.Livros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);
        }

        public Entidades.Livro SalvarLivro(Entidades.Livro livro)
        {
            var livroDb = this.context.Livros.FirstOrDefault(x => x.ISBN == livro.ISBN);

            if (livroDb != null)
                throw new BusinessException("ISBN já cadastrado na base de dados! Por favor, utilize outro.");

            this.context.Livros.Add(livro);
            this.context.SaveChanges();

            return livro;
        }

        public Entidades.Livro AtualizarLivro(Guid id, Entidades.Livro livro)
        {
            var livroDb = this.context.Livros.FirstOrDefault(x => x.Id == id);

            if (livroDb == null)
                throw new BusinessException("Livro não encontrado! Não é possível realizar uma atualização.");

            livroDb.Titulo = livro.Titulo;
            livroDb.ISBN = livro.ISBN;
            livroDb.Ano = livro.Ano;
            livroDb.AutorId = livro.AutorId;

            this.context.Livros.Update(livroDb);
            this.context.SaveChanges();

            return livroDb;
        }

        public void ExcluirLivro(Entidades.Livro livro)
        {
            this.context.Livros.Remove(livro);
            this.context.SaveChanges();
        }
    }
}
