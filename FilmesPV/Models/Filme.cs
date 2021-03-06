using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesPV.Models
{
    public class Filme : Base
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Autor { get; set; }
        public Categoria Categoria { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Resumo { get; set; }

        [NotMapped]
        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }

        public string Imagem { get; set; }
    }
}
