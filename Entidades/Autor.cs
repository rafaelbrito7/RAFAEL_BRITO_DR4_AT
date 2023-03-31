using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entidades
{
    public class Autor
    {
        public Autor()
        {
            this.Livros = new List<Livro>();
        }

        [Key]
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [JsonPropertyName("sobrenome")]
        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        public string Sobrenome { get; set; }

        [JsonPropertyName("nomeCompleto")]
        public string NomeCompleto => $"{Nome} {Sobrenome}";

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [JsonPropertyName("dtNascimento")]
        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        public DateTime DtNascimento { get; set; }

        public ICollection<Livro> Livros { get; set; }
    }
}