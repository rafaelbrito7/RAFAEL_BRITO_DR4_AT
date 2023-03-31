using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        [Key]
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        [Required(ErrorMessage = "Nome é Obrigatório")]
        public string Nome { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Email é Obrigatório")]
        [EmailAddress(ErrorMessage = "Email não está em um formato correto")]
        public String Email { get; set; }

        [JsonPropertyName("dtNascimento")]
        [Required(ErrorMessage = "Data de nascimento é Obrigatório")]
        public DateTime DtNascimento { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Senha é Obrigatório")]
        public string Password { get; set; }

        public void CriptografarPassword()
        {
            this.Password = Convert.ToBase64String(Encoding.Default.GetBytes(this.Password));
        }
    }
}


