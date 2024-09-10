using _0411.Model;
using System.ComponentModel.DataAnnotations;

namespace _0411.Dtos
{
    public class TipoQuartoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(100, ErrorMessage = "Tamanho entre 4 a 100 caracteres", MinimumLength = 4)]
        public string? Descricao { get; set; }
        
        [Range(2.00, 300.00, ErrorMessage = "O preço deve estar entre 2,00 e 300,00")]
        public decimal Valor_Diaria { get; set; }
    }

    public class QuartoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Número do quarto é obrigatório")]
        public int Numero_Quarto { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Tipo de quarto é obrigatório")]
        public int TipoQuarto_Id { get; set; }
    }

    public class ProdutoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Valor { get; set; }
    }
}
