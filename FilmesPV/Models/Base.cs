using System;
using System.ComponentModel;

namespace FilmesPV.Models
{
    public abstract class Base
    {
        public int Id { get; set; }
        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

    }
}
