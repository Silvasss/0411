using System.ComponentModel.DataAnnotations;

namespace _0411.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public InformacoesEmpregoDto? InformacoesEmprego { get; set; }
        public InformacoesUsuarioDto? InformacoesUsuario { get; set; }
    }
    public partial class InformacoesEmpregoDto
    {
        public int Id { get; set; }
        public DateTime? Data_Admissao { get; set; }
        public decimal? Salario { get; set; }
        public string? Status { get; set; }
    }
    public partial class InformacoesUsuarioDto
    {
        public int Id { get; set; }
        public string? Telefone { get; set; }
        public string? Rg { get; set; }
        public string? Cpf { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
    }
    public partial class CadastroUsuarioDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Usuário é obrigatório")]
        [StringLength(32, ErrorMessage = "Tamanho entre 4 a 32 caracteres", MinimumLength = 4)]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [StringLength(16, ErrorMessage = "Tamanho entre 4 a 16 caracteres", MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;
    }
    public partial class LoginDto
    {
        [Required(ErrorMessage = "Usuário é obrigatório")]
        [StringLength(32, ErrorMessage = "Tamanho entre 4 a 32 caracteres", MinimumLength = 4)]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [StringLength(16, ErrorMessage = "Tamanho entre 4 a 16 caracteres", MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;
    }
}
