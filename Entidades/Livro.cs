using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entidades
{
    public class Livro
    {
        [Key]
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [JsonPropertyName("titulo")]
        [Required(ErrorMessage = "Titulo é Obrigatório")]
        public string Titulo { get; set; }

        [JsonPropertyName("isbn")]
        [Required(ErrorMessage = "ISBN é Obrigatório")]
        public string ISBN { get; set; }

        [JsonPropertyName("ano")]
        [Required(ErrorMessage = "Ano é Obrigatório")]
        public int Ano { get; set; }

        [JsonPropertyName("autorId")]
        public Guid AutorId { get; set; }

        [JsonPropertyName("autor")]
        public Autor? Autor { get; set; }
    }
}