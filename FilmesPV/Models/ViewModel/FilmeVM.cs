using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FilmesPV.Models.ViewModel
{
    public class FilmeVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "O campo Categoria é obrigatório")]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Resumo { get; set; }
        public string Imagem { get; set; }
        public string Teste { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }
    }
}
