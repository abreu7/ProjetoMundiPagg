using System.ComponentModel.DataAnnotations;

namespace MundiPagg.API.Dtos
{
    public class ProdutoDto
    {
        public string Id { get; set; }

        [Required (ErrorMessage="O Campo {0} é obrigatório")]
        [StringLength (100, MinimumLength=3, ErrorMessage="Mínimo 3 caracters")]
        public string Nome { get; set; }
        
        [Required (ErrorMessage="O Campo {0} é obrigatório")]
        public string Descricao { get; set; }
        public string Url { get; set; }
        
        [Required (ErrorMessage="O Campo {0} é obrigatório")]
        public string Categoria { get; set; }
        
        [Required (ErrorMessage="O Campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [Required (ErrorMessage="O Campo {0} é obrigatório")]
        public string Marca { get; set; }

    }
}