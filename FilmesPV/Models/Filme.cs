namespace FilmesPV.Models
{
    public class Filme : Base
    {
        public string Nome { get; set; }
        public string Autor { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }
        public decimal Preco { get; set; }
        public string Resumo { get; set; }
        public string Imagem { get; set; }
    }
}
